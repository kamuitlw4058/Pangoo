
using System;

namespace Pangoo.Common
{
    public static class DateTimeUtility
    {

        public static string FormatDateTimeNow()
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("yyyyMMddHHmmssff");
            return formattedTime;
        }

        public static DateTime ParseDateTimeNow(string dat)
        {
            return DateTime.ParseExact(dat, "yyyyMMddHHmmssff", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}