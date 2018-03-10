using System;
using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;
using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Parsers
{
    public class InTheaterNowParser : BaseParser, IParser
    {
        public List<ScrapedData> Parse(HtmlBag htmlBag)
        {
            if (htmlBag == null)
                throw new ArgumentException();

            var docs = htmlBag.Bag;
            var parsedData = new List<ScrapedData>();

            foreach (var day in docs.Keys)
            {
                var nodes = GetMovieNodes(docs[day]);

                nodes.ForEach((node) =>
                {
                    var parsed = new NodeParser(EnumHelper.GetEnum<DayOfWeek>(day), node).Parse();
                    parsedData.Add(parsed);
                    LogHelper.Information(this, $"Fragment parsed {day}_{parsed?.SiteMovieId}_{parsed?.Showtimes?.Count}_{nodes?.Count}");
                });
            }
            return parsedData;
        }

        private List<HtmlNode> GetMovieNodes(HtmlDocument doc)
        {
            try
            {
                return doc.DocumentNode.SelectNodes("//div[@class='filmic']").ToList();
            }
            catch (Exception e)
            {
                LogHelper.Error(this, e.ToString());
                throw;
            }
        }
    }

    public class NodeParser
    {
        DayOfWeek day;
        HtmlNode movieNode;

        public NodeParser(DayOfWeek day, HtmlNode movieNode)
        {
            this.day = day;
            this.movieNode = movieNode ?? throw new ArgumentException("MovieNode is empty.");
        }

        public ScrapedData Parse()
        {
            try
            {
                return new ScrapedData
                {
                    PosterLink = movieNode.SelectSingleNode(".//img[@class='fotka']").GetAttributeValue("src", String.Empty),
                    SiteMovieId = movieNode.SelectSingleNode(".//img[@class='fotka']")
                                           .NextSibling
                                           .NextSibling
                                           .SelectSingleNode(".//a")
                                           .GetAttributeValue("href", String.Empty)
                                           .OnlyNumbers(),

                    TitleBA = movieNode.SelectSingleNode(".//img[@class='fotka']")
                                       .NextSibling
                                       .NextSibling
                                       .SelectSingleNode(".//a")
                                       .GetAttributeValue("title", String.Empty),

                    Title = movieNode.SelectNodes(".//p/strong")[0].NextSibling.InnerText.Trim(),
                    Director = movieNode.SelectNodes(".//p/strong")[1].NextSibling.InnerText.Trim(),
                    Cast = movieNode.SelectNodes(".//p/strong")[2].NextSibling.InnerText.Trim(),
                    Genre = movieNode.SelectNodes(".//p/strong")[3].NextSibling.InnerText.Trim(),
                    Duration = movieNode.SelectSingleNode(".//h5").InnerText.OnlyNumbers(),
                    Showtimes = ParseTimes()
                };
            }
            catch (Exception e)
            {
                LogHelper.Error(this, e.StackTrace?.ToString());
                throw;
            }
        }

        private List<Tuple<DayOfWeek, string>> ParseTimes()
        {
            List<Tuple<DayOfWeek, string>> times = new List<Tuple<DayOfWeek, string>>();

            var timeNodes = movieNode.SelectNodes(".//div[@class='termin']/ul[@class='dan']/li");

            if (timeNodes == null)
                return times;

            foreach (var item in timeNodes)
            {
                var time = item.InnerText.OnlyHours();
                if (time == "")
                    continue;

                times.Add(new Tuple<DayOfWeek, string>(day, time));
            }

            return times;
        }


    }
}
