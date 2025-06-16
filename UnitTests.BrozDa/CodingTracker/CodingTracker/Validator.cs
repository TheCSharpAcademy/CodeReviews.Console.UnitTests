using System.Globalization;
using System.Runtime.Serialization;

namespace CodingTracker
{
    internal static class Validator
    {
        public static bool IsValidDate(string input, string dateTimeFormat)
        {
            return DateTime.TryParseExact(input, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
        public static bool IsValidEndDate(string input, DateTime startDate, string dateTimeFormat)
        {
            if(DateTime.TryParseExact(input, dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedInput))
            {
                return parsedInput >= startDate;
            }

            return false;
        }
        public static bool IsSessionIdValidOrZero(int input, HashSet<int> validIds)
        {
            return validIds.Contains(input) || input == 0;
        }
    }
}
