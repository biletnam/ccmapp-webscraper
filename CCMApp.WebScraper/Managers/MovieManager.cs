using System;
using CCMApp.WebScraper.Models;
using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;

namespace CCMApp.WebScraper.Managers
{
    public class MovieManager : IMovieManager
    {
        Movie movie;

        public Movie Create(ScrapedData scrapedData)
        {
            movie = new Movie();
            BindData(scrapedData);

            return movie;
        }

        private void BindData(ScrapedData scrapedData)
        {
            movie.Id = Guid.NewGuid().ToString();
            movie.PosterLink = scrapedData.PosterLink;
            movie.Title = scrapedData.Title;
            movie.TitleBA = scrapedData.TitleBA;
            movie.Duration = scrapedData.Duration;
            movie.Genre = scrapedData.Genre;
            movie.Director = scrapedData.Director;
            movie.Cast = scrapedData.Cast;
            movie.SiteMovieId = scrapedData.SiteMovieId;
            movie.Storyline = scrapedData.Storyline;
            movie.PhotoUrl = Urls.GetPhotoUrlFor(scrapedData.PhotoUrl);
            movie.VideoUrl = Urls.GetYtUrlFor(scrapedData.VideoUrl);

            LogHelper.Information(this, $"Movie created ({movie?.Title}) {movie?.SiteMovieId}");
        }
    }
}
