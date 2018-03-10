using System.Collections.Generic;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface IScrapedDataManager
    {
        List<ScrapedData> TidyUp(List<ScrapedData> list);
    }
}
