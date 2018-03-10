using System;

namespace CCMApp.WebScraper.Infrastructure
{
    public interface IDateBuilder
    {
        DateTime First { get; }
        DateTime Today { get; }

        DateTime GetDate(DayOfWeek day, string time);
    }
}
