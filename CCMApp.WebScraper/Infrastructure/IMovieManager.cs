using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface IMovieManager
    {
        Movie Create(ScrapedData fragment);
    }
}