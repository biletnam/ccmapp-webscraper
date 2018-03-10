using System.Collections.Generic;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Loaders;
using CCMApp.WebScraper.Parsers;
using CCMApp.WebScraper.Managers;

namespace CCMApp.WebScraper
{
    public class ScraperBuilder
    {
        private Dictionary<ILoader, IParser> loaderParserDict = new Dictionary<ILoader, IParser>();
        private Dictionary<ISecLoader, ISecParser> loaderParser2ndDict = new Dictionary<ISecLoader, ISecParser>();
        private IScrapedDataManager dataManager;

        public static ScraperBuilder Builder() => new ScraperBuilder();

        public ScraperBuilder WithDefaultLoadersParsers() =>
            new ScraperBuilder { loaderParserDict = DefaultLoaderParser(), loaderParser2ndDict = DefaultLoaderParser2nc() };

        public ScraperBuilder WithDefaultScrapedDataManager() =>
            new ScraperBuilder { loaderParserDict = this.loaderParserDict, loaderParser2ndDict = this.loaderParser2ndDict,
                dataManager = new ScrapedDataManager() };

        public ScraperBuilder WithLoaderParser(ILoader loader, IParser parser)
        {
            loaderParserDict.Add(loader, parser);
            return new ScraperBuilder { loaderParserDict = this.loaderParserDict, loaderParser2ndDict = this.loaderParser2ndDict };
        }

        public ScraperBuilder WithLoaderParser(ISecLoader loader, ISecParser parser)
        {
            loaderParser2ndDict.Add(loader, parser);
            return new ScraperBuilder { loaderParserDict = this.loaderParserDict, loaderParser2ndDict = this.loaderParser2ndDict };
        }

        public Scraper Build() => new Scraper(loaderParserDict, loaderParser2ndDict, dataManager);

        private Dictionary<ILoader, IParser> DefaultLoaderParser() =>
            new Dictionary<ILoader, IParser> { { new InTheaterNowLoader(), new InTheaterNowParser() } };
        private Dictionary<ISecLoader, ISecParser> DefaultLoaderParser2nc() =>
            new Dictionary<ISecLoader, ISecParser> { { new MovieDetailsLoader(), new MovieDetailsParser() } };
    }
}
