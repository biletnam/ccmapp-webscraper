using HtmlAgilityPack;
using System.Collections.Generic;

namespace CCMApp.WebScraper
{
    public class HtmlBag
    {
        Dictionary<string, HtmlDocument> bag = new Dictionary<string, HtmlDocument>();
        public Dictionary<string, HtmlDocument> Bag => bag;

        public void Add(string identifier, HtmlDocument html) => bag.Add(identifier, html);
        public HtmlDocument Get(string identifier) => bag[identifier];
        public void Clear() => bag.Clear();

        public void JoinBag(HtmlBag htmlBag)
        {
            var keys = htmlBag.bag.Keys;
            foreach (var key in keys)
                bag.Add(key, htmlBag.bag[key]);
        }
    }
}
