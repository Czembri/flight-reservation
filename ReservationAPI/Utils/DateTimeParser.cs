namespace ReservationAPI.Utils
{
    public static class DateTimeParser
    {
        public static DateTime Parse(string dateTime)
        {
            if (!DateTime.TryParseExact(
                dateTime,
                "yyyy-MM-ddTHH:mm:ss",
                null,
                System.Globalization.DateTimeStyles.None,
                out var result))
            {
                throw new System.FormatException("Invalid date format");
            }

            return result;
        }
    }
}
