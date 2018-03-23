using System;

namespace XMedia.Utils
{
    public static class ExtensionsDate
    {
        public static DateTime ToShortDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }
    }
}
