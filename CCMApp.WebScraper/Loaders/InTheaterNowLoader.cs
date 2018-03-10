using CCMApp.WebScraper.Helpers;
using CCMApp.WebScraper.Infrastructure;

namespace CCMApp.WebScraper.Loaders
{
    public class InTheaterNowLoader : BaseLoader, ILoader
    {
        public HtmlBag Load()
        {
            var htmlBag = new HtmlBag();

            foreach (var day in DateBuilder.DaysOfWeek)
            {
                htmlBag.Add(day.ToString(), GetHtml(Urls.GetUrlFor(day)));
                LogHelper.Information(this, $"Loading of InTheaterNow html for {day.ToString()} has finished");
            }

            return htmlBag;
        }
    }
}
