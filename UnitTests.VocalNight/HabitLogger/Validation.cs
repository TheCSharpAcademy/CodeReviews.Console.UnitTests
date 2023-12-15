using System.Globalization;

namespace HabitLogger
{
    public static class Validation
    {
        public static Boolean ValidateDate( string date )
        {
            if (string.IsNullOrWhiteSpace(date)) return false;

            if (DateTime.TryParseExact(date, "dd-MM-yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                return true;
            }

            return false;
        }
    }
}
