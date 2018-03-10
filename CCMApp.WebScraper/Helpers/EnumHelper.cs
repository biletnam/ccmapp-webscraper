using System;

namespace CCMApp.WebScraper.Helpers
{
    public static class EnumHelper
    {
        public static T GetEnum<T>(string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch (Exception e)
            {
                LogHelper.Error("EnumHelper", $"Enum parsing failed for {typeof(T).ToString()}, {e?.Message}");
                throw;
            }
        }
    }
}
