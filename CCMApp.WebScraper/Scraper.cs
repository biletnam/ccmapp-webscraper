using System.Collections.Generic;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper
{
    public class Scraper
    {
        private Dictionary<ILoader, IParser> loaderParserDict;
        private Dictionary<ISecLoader, ISecParser> loaderParser2ndDict;
        private IScrapedDataManager dataManager;

        List<ScrapedData> scrapedData = new List<ScrapedData>();

        public Scraper(Dictionary<ILoader, IParser> loaderParserDict, Dictionary<ISecLoader, 
            ISecParser> loaderParser2ndDict, IScrapedDataManager dataManager)
        {
            this.loaderParserDict = loaderParserDict;
            this.loaderParser2ndDict = loaderParser2ndDict;
            this.dataManager = dataManager;
        }

        public List<ScrapedData> Scrape()
        {
            foreach (KeyValuePair<ILoader, IParser> pair in loaderParserDict)
            {
                var loader = pair.Key;
                var parser = pair.Value;

                var html = loader.Load();
                var parsed = parser.Parse(html);
                scrapedData.AddRange(parsed);
            };

            scrapedData = dataManager.TidyUp(scrapedData);

            foreach (KeyValuePair<ISecLoader, ISecParser> pair in loaderParser2ndDict)
            {
                var loader = pair.Key;
                var parser = pair.Value;

                var html = loader.Load(scrapedData);
                parser.Parse(html, scrapedData);
            };

            return dataManager.TidyUp(scrapedData);
        }
    }


}
