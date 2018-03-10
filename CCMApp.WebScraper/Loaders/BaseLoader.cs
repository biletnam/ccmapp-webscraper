using System;
using HtmlAgilityPack;
using CCMApp.WebScraper.Helpers;

namespace CCMApp.WebScraper.Loaders
{
    public abstract class BaseLoader
    {
        HtmlWeb htmlWeb;

        public BaseLoader() => htmlWeb = new HtmlWeb();

        protected HtmlDocument GetHtml(string url)
        {
            try
            {
                return htmlWeb.Load(url);
            }
            catch (Exception e)
            {
                LogHelper.Error(this, e.ToString());
                throw;
            }
        }
    }
}
