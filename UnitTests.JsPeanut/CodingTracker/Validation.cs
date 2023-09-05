using System;
using System.Globalization;

namespace CodingTracker
{
    public static class Validation
    {
        public static bool ValidateDate(string date, string format)
        {
            bool tryParseStartDateString = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);

            if (!tryParseStartDateString)
            {
                Console.WriteLine("Invalid format");

                return false;
            }

            return true;
        }

        public static bool ValidateNumber(string numberInput)
        {
            while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
            {
                Console.WriteLine("Invalid format");

                return false;
            }

            return true;
        }

        public static bool ValidateDuration(string startTime, string endTime, string format)
        {
            if (DateTime.ParseExact(endTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None) < DateTime.ParseExact(startTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None))
            {
                Console.WriteLine("Invalid format. The time in which your session ended, was before your session even started.");

                return false;
            }

            return true;
        }
    }
}