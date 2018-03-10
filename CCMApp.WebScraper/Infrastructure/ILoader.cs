using System.Collections.Generic;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface ILoader
    {
        HtmlBag Load();
    }

    public interface ISecLoader
    {
        HtmlBag Load(List<ScrapedData> data);
    }
}
