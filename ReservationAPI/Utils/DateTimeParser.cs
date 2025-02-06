using System;
using System.Globalization;

namespace ReservationAPI.Utils
{
    public static class DateTimeParser
    {
        public static DateTime Parse(string dateTime)
        {
            if (!DateTime.TryParseExact(
                dateTime,
                "yyyy-MM-ddTHH:mm:ss.fffZ",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out var result))
            {
                throw new FormatException("Invalid date format");
            }

            return result;
        }

        public static DateTime Parse2(string dateTime)
        {
            if (!DateTime.TryParseExact(
                dateTime,
                "yyyy-MM-ddTHH:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AdjustToUniversal,
                out var result))
            {
                throw new FormatException("Invalid date format");
            }

            return result;
        }
    }
}
