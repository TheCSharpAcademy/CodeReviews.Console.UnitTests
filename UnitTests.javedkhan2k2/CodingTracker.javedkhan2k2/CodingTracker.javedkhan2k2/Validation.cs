
using System.Globalization;

namespace CodingTracker;

public static class Validation
{
    public static bool IsValidDateInput(string? date, string? format)
    {
        if (date != null && date == "0")
        {
            return true;
        }
        if (date == null || !DateTime.TryParseExact(date, format, new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return false;
        }
        return true;
    }

    public static bool IsValidDateTimeInputs(string startDate, string endDate, string format)
    {
        if (AreDatesEmptyOrZero(startDate, endDate) || 
            !DateTime.TryParseExact(startDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out _) ||
            !DateTime.TryParseExact(endDate, format, new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return false;
        }
        DateTime startDateTime = DateTime.Parse(startDate);
        DateTime endDateTime = DateTime.Parse(endDate);
        return DateTime.Compare(endDateTime, startDateTime) > 0 ? true : false;
    }

    public static bool AreValidYearInputs(string startDate, string endDate)
    {
        if (AreDatesEmptyOrZero(startDate, endDate) 
            || !IsPostiveIntegerInput(Int32.Parse(startDate)) 
            || !IsPostiveIntegerInput(Int32.Parse(endDate)))
        {
            return false;
        }
        return Int32.Parse(startDate) <= Int32.Parse(endDate) ? true : false;
    }

    public static bool AreDatesEmptyOrZero(string startDate, string endDate)
    {
        if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate)
            || startDate == "0" || endDate == "0")
        {
            return true;
        }
        return false;
    }

    public static bool IsValidIntegerInput(string? userInput)
    {
        if (userInput == null || !Int32.TryParse(userInput, out int result) || !IsPostiveIntegerInput(result))
        {
            return false;
        }
        return true;
    }

    public static bool IsPostiveIntegerInput(int result) => result >= 0;

}