using System;
using System.Linq;
using System.Globalization;
using System.Configuration;
using System.Collections.Generic;
using CCMApp.WebScraper.Infrastructure;

namespace CCMApp.WebScraper
{
    public class DateBuilder : IDateBuilder
    {
        string timezone = "Central European Standard Time";
        int programPublishedAt = 20;

        Dictionary<DayOfWeek, DateTime> dateDictionary = new Dictionary<DayOfWeek, DateTime>();

        DateTime today;
        public DateTime Today
        {
            get => today;
            set
            {
                var v = value;

                //if Wednesday and hour > 20:00, new program is available on webpage starting Thursday
                if (v.DayOfWeek == DayOfWeek.Wednesday && v.Hour >= programPublishedAt)
                    v = v.AddDays(1);
                today = v;
            }
        }

        public DateTime First => dateDictionary.OrderBy(d => d.Value).FirstOrDefault().Value;

        public static IEnumerable<DayOfWeek> DaysOfWeek
        {
            get
            {
                foreach (var value in Enum.GetValues(typeof(DayOfWeek)))
                    yield return (DayOfWeek)value;
            }
        }

        public DateBuilder(string timezone)
            : this()
        {
            this.timezone = timezone;
        }

        public DateBuilder()
        {
            Today = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timezone));

            // Theater program is replaced with new every Thursday. 
            // App creates and stores dates to be able to parse showtimes
            CreateDateDictionary(); 
        }

        public DateTime GetDate(DayOfWeek day, string time)
        {
            DateTime.TryParse($"{dateDictionary[day].Date.ToString(@"MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo)} {time}", out DateTime date);
            return date;
        }

        private void CreateDateDictionary()
        {
            (int before, int after) = CalculateDaysBeforeAfter();

            while (before > 0)
            {
                var date = today.Subtract(TimeSpan.FromDays(before));
                dateDictionary.Add(date.DayOfWeek, date);
                before--;
            }

            dateDictionary.Add(today.DayOfWeek, today);

            for (int i = 1; i < after + 1; i++)
            {
                var date = today.AddDays(i);
                dateDictionary.Add(date.DayOfWeek, date);
            }
        }

        private (int before, int after) CalculateDaysBeforeAfter()
        {
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return (4, 2);
                case DayOfWeek.Tuesday:
                    return (5, 1);
                case DayOfWeek.Wednesday:
                    return (6, 0);
                case DayOfWeek.Thursday:
                    return (0, 6);
                case DayOfWeek.Friday:
                    return (1, 5);
                case DayOfWeek.Saturday:
                    return (2, 4);
                case DayOfWeek.Sunday:
                    return (3, 3);
                default:
                    return (0, 0);
            }
        }
    }
}
