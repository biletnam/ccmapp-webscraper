using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Managers;
using CCMApp.WebScraper.Models;
using CCMApp.WebScraper.Helpers;

namespace CCMApp.WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Movie> movies = new List<Movie>();
            IMovieManager movieManager = new MovieManager();

            var scraped = ScraperBuilder.Builder()
                                        .WithDefaultLoadersParsers()
                                        .WithDefaultScrapedDataManager()
                                        .Build()
                                        .Scrape();

            foreach (var scrapedItem in scraped)
            {
                var movie = movieManager.Create(scrapedItem);

                var showtimeManager = new ShowtimeManager(movie, new DateBuilder());
                showtimeManager.UpdateShowtimes(scrapedItem);

                movies.Add(movie);
            }

            try
            {
                // sample of scraped data can be found in ./scrapedData.json
                var json = JsonConvert.SerializeObject(movies, Formatting.Indented);
            }
            catch (Exception e)
            {
                LogHelper.Error("Program", e.ToString());
                throw;
            }

        }
    }

}

