using System;
using System.Collections.Generic;

namespace CCMApp.WebScraper
{
    public class Urls
    {
        const string url = @"http://www.cinemacity.ba";
        const string urlDay = @"index.php?dan=";
        const string urlMovie = @"filmovi.php?film_id=";
        const string youtubeUrl = "https://www.youtube.com/watch?v=";

        // day names in bosnian
        static Dictionary<DayOfWeek, string> days = new Dictionary<DayOfWeek, string>()
        {
            { DayOfWeek.Monday, "Ponedjeljak"},
            { DayOfWeek.Tuesday, "Utorak"},
            { DayOfWeek.Wednesday, "Srijeda"},
            { DayOfWeek.Thursday, "Cetvrtak"},
            { DayOfWeek.Friday, "Petak"},
            { DayOfWeek.Saturday, "Subota"},
            { DayOfWeek.Sunday, "Nedjelja"},
        };

        public static string GetUrlFor(DayOfWeek day) => $@"{url}/{urlDay}{days[day]}";
        public static string GetUrlFor(string siteMovieId) => $@"{url}/{urlMovie}{siteMovieId}";
        public static string GetPhotoUrlFor(string photoUrl) => $@"{url}/{photoUrl}";
        public static string GetYtUrlFor(string videoUrl) => $@"{youtubeUrl}{videoUrl}";
    }
}
