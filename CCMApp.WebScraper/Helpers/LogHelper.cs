using System;
using System.Diagnostics;

namespace CCMApp.WebScraper.Helpers
{
    public static class LogHelper
    {      
        public static void Information(object sender, string msg) => Trace.TraceInformation($"{sender} ({Time()}): {msg}");
        public static void Warning(object sender, string msg) => Trace.TraceWarning($"{sender} ({Time()}): {msg}");
        public static void Error(object sender, string msg) => Trace.TraceError($"{sender} ({Time()}): {msg}");
        private static string Time() => DateTime.UtcNow.ToString("hh:mm:ss tt");
    }
}
