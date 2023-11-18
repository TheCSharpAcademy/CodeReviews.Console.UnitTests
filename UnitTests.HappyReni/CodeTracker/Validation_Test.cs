using System.Globalization;
using System.Text.RegularExpressions;

namespace CodeTracker
{
    public static class Validation_Test
    {
        public static bool ValidDateTime(string date)
        {
            string format = "yyyy-MM-dd HH:mm:ss";

            return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
        public static bool ValidYear(int year) => year >= 0 && year <= 9999;

        public static bool ValidWeek(string week)
        {
            Regex regex = new Regex(@"[0-9]{4}-(0[1-9]|[1-4][0-9]|5[0-2])");
            return regex.IsMatch(week);
        }
        public static bool ValidDate(string date)
        {
            string format = "yyyy-MM-dd";

            return DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
