using System;
using System.Linq;
using System.Collections.Generic;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;
using CCMApp.WebScraper.Helpers;

namespace CCMApp.WebScraper.Managers
{
    public class ShowtimeManager : IShowtimeManager
    {
        Movie movie;
        IDateBuilder dateBuilder;

        public ShowtimeManager(Movie movie, IDateBuilder dateBuilder)
        {
            this.movie = movie;
            this.dateBuilder = dateBuilder;
        }

        public void UpdateShowtimes(ScrapedData scrapedData)
        {
            var scrapedShowtimes = new List<Showtime>();

            foreach (var time in scrapedData.Showtimes)
                scrapedShowtimes.Add(CreateShowtime(time));

            // exclude showtimes already in movie.showtimes
            var difShowtimes = scrapedShowtimes.Except(movie.Showtimes, new ShowtimeEqualityComparer());

            foreach (var item in difShowtimes)
                movie.Showtimes.Add(item);

            LogHelper.Information(this, $"Showtimes compared fs_{scrapedShowtimes.Count}  totals_{movie.Showtimes.Count}");
        }

        private Showtime CreateShowtime(Tuple<DayOfWeek, string> date)
        {
            var showtime = new Showtime { Id = Guid.NewGuid().ToString(), DateTime = dateBuilder.GetDate(date.Item1, date.Item2) };
            LogHelper.Information(this, $"Showtime created ({date.Item1.ToString()},{date.Item2}): {showtime.DateTime.ToString()}");

            return showtime;
        }
    }

    public class ShowtimeEqualityComparer : IEqualityComparer<Showtime>
    {
        public bool Equals(Showtime x, Showtime y) => x.DateTime.Equals(y.DateTime);
        public int GetHashCode(Showtime obj) => obj.DateTime.GetHashCode();
    }
}
