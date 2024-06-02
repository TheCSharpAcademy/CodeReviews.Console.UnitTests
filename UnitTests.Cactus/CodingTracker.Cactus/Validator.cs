using System.Globalization;

namespace ConConfig
{
    public class Validator
    {
        public static bool IsValidDate(string dateStr, out DateTime date)
        {
            if (DateTime.TryParseExact(dateStr, "HH:mm dd-MM-yyyy", new CultureInfo("en-US"),
                                   DateTimeStyles.None, out date))
            {
                return true;
            }
            return false;
        }
    }
}