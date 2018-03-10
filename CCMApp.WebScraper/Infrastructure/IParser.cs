using System.Collections.Generic;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface IParser
    {
        List<ScrapedData> Parse(HtmlBag htmlBag);
    }

    public interface ISecParser
    {
        List<ScrapedData> Parse(HtmlBag htmlBag, List<ScrapedData> data);
    }
}
