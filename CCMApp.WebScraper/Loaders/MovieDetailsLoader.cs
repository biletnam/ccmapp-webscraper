using System;
using System.Linq;
using System.Collections.Generic;
using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;
using CCMApp.WebScraper.Models;

namespace CCMApp.WebScraper.Loaders
{
    public class MovieDetailsLoader : BaseLoader, ISecLoader
    {
        public HtmlBag Load(List<ScrapedData> data)
        {
            if (data == null)
                throw new ArgumentException();

            var htmlBag = new HtmlBag();

            //get unique ids 
            var ids = data.GroupBy(m => m.SiteMovieId).Select(x => x.Key);

            foreach (var id in ids)
            {
                htmlBag.Add(id, GetHtml(Urls.GetUrlFor(id)));
                LogHelper.Information(this, $"Loading of Movie details - html for {id} has finished");
            }
            return htmlBag;
        }
    }
}
