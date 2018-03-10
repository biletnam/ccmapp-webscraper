using System.Collections.Generic;

namespace CCMApp.WebScraper.Models
{
    public class Movie
    {
        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
        }

        public string Id { get; set; }
        public string PosterLink { get; set; }
        public string Title { get; set; }
        public string TitleBA { get; set; }
        public string Duration { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string SiteMovieId { get; set; }

        public string Storyline { get; set; }
        public string PhotoUrl { get; set; }
        public string VideoUrl { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
    }
}
