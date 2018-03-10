using System.Text.RegularExpressions;

namespace CCMApp.WebScraper.Helpers
{
    public static class StringHelper
    {
        public static string  OnlyNumbers(this string s) => new Regex(@"\d+").Match(s).Value;
        public static string OnlyHours(this string s) => new Regex(@"\d+:\d+").Match(s).Value;
        public static string RemoveSpecial(this string s) => new Regex(@"(\n|\r|\r\n)").Replace(s, " ");
        public static string OnlyYtLink(this string s) => Regex.Split(Regex.Split(s, "&")[0], @"/")[4];
        public static string GuidLastEight(this string s) => Regex.Split(s, "-")[4];
    }
}
