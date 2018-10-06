using System;
using System.Globalization;

namespace ShopService.Common.Exceptions
{
    public static class TimeExtension
    {
        private const string ZonedDateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffzzz";
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fff";

        private static DateTime _epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static long ToUnixEpochTime(this DateTime dateTime)
        {
            return (long)Math.Round((dateTime - _epochTime).TotalMilliseconds);
        }

        public static long? ToUnixEpochTime(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return null;
            return (long)Math.Round((dateTime.Value - _epochTime).TotalMilliseconds);
        }

        public static DateTime? ToDateTime(this long? date)
        {
            if (!date.HasValue) return null;
            return _epochTime.AddMilliseconds(date.Value);
        }

        public static DateTime ToDateTime(this long date)
        {
            return _epochTime.AddMilliseconds(date);
        }
        
        public static DateTime TrimMiliseconds(this DateTime dateTime)
        {
            return dateTime.AddMilliseconds(-dateTime.Millisecond);
        }
        
        public static DateTimeOffset TrimMiliseconds(this DateTimeOffset dateTime)
        {
            return dateTime.AddMilliseconds(-dateTime.Millisecond);
        }
        
        public static string ToFormatString(this DateTime dateTime)
        {
            return dateTime.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
        }
        
        public static string ToIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString(ZonedDateTimeFormat, CultureInfo.InvariantCulture);
        }
        
        public static DateTime? ToDateTimeIfNotNull(this string value)
        {
            if (value == null) return null;
            return DateTime.Parse(value);
        }

        public static DateTime ToDateTime(this string value)
        {
            return DateTime.Parse(value);
        }
    }
}