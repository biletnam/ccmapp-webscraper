using System;
using System.Linq;
using System.Collections.Generic;
using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Parsers
{
    public class MovieDetailsParser : BaseParser, ISecParser
    {
        public List<ScrapedData> Parse(HtmlBag htmlBag, List<ScrapedData> data)
        {
            if (htmlBag == null || data == null)
                throw new ArgumentException();

            var docs = htmlBag.Bag;

            foreach (var siteMoveId in docs.Keys)
            {
                try
                {
                    var sd = data.Where(x => x.SiteMovieId == siteMoveId).SingleOrDefault();

                    if (sd == null)
                        continue;

                    var nodes = docs[siteMoveId].DocumentNode.SelectNodes("//div[@class='film']/h4").ToList();

                    sd.Storyline = nodes[0].NextSibling.NextSibling.InnerText.RemoveSpecial();

                    sd.PhotoUrl = nodes[1].NextSibling
                                          .NextSibling
                                          .SelectSingleNode(".//img[@id='img1']")
                                          .GetAttributeValue("src", String.Empty);

                    sd.VideoUrl = nodes[2].NextSibling
                                          .NextSibling
                                          .SelectSingleNode(".//object/param[@name='movie']")
                                          .GetAttributeValue("value", String.Empty)
                                          .OnlyYtLink();

                    LogHelper.Information(this, $"Movie html parsed ({sd.PhotoUrl}, {sd.VideoUrl})");
                }
                catch (Exception e)
                {
                    LogHelper.Error(this, e.ToString());
                }
            }
            return data;
        }
    }
}
