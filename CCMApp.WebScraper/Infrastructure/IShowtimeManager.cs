using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface IShowtimeManager
    {
        void UpdateShowtimes(ScrapedData fragment);
    }
}