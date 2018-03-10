using System;
using System.Linq;
using System.Collections.Generic;
using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Managers
{
    public class ScrapedDataManager : IScrapedDataManager
    {
        public List<ScrapedData> TidyUp(List<ScrapedData> list)
        {
            List<ScrapedData> optimized = new List<ScrapedData>();

            //select unique ids
            var movieIds = list.GroupBy(s => s.SiteMovieId).Select(m => m.Key);

            foreach (var movieId in movieIds)
            {
                var sameId = list.Where(s => s.SiteMovieId == movieId).ToList();

                if (sameId.Count == 1)
                    optimized.Add(sameId[0]);

                if (sameId.Count != 1)
                    optimized.Add(ShrinkIt(sameId)); 
            }

            return optimized; // duplicates are removed, list has unique ids 
        }

        private ScrapedData ShrinkIt(List<ScrapedData> sameId)
        {
            ScrapedData data = new ScrapedData
            {
                PosterLink = sameId[0]?.PosterLink,
                SiteMovieId = sameId[0]?.SiteMovieId,
                TitleBA = sameId[0]?.TitleBA,
                Title = sameId[0]?.Title,
                Director = sameId[0]?.Director,
                Cast = sameId[0]?.Cast,
                Genre = sameId[0]?.Genre,
                Duration = sameId[0]?.Duration,
                Storyline = sameId[0]?.Storyline,
                PhotoUrl = sameId[0]?.PhotoUrl,
                VideoUrl = sameId[0]?.VideoUrl,
                Showtimes = new List<Tuple<DayOfWeek, string>>()
            };

            foreach (var item in sameId)
                data.Showtimes.AddRange(item.Showtimes);

            LogHelper.Information(this, $"Shrinked({sameId?.Count})fragments({data?.SiteMovieId}_{data?.Showtimes?.Count})");
            return data;
        }
    }
}
