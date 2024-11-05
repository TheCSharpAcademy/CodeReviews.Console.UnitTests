namespace CodingTracker.Arashi256.Classes
{
    public class Validator
    {
        private const string DateFormat = "dd-MM-yy HH:mm:ss";

        public bool CheckDates(DateTime start, DateTime end)
        {
            return start <= end;
        }

        public bool TryParseDate(string dateString, out DateTime result)
        {
            return DateTime.TryParseExact(dateString, DateFormat, null, System.Globalization.DateTimeStyles.None, out result);
        }
    }
}
