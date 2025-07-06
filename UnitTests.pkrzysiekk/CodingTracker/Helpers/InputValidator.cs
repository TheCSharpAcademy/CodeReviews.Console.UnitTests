using System.Globalization;

namespace CodingTracker.Controllers;

public static class InputValidator
{
    public static bool AreDatesValid(DateTime startDate, DateTime endDate)
    {
        return endDate > startDate && startDate <= DateTime.Now && endDate <= DateTime.Now;
    }

    public static bool IsDateValid(string date)
    {
        var isParsable = DateTime.TryParseExact(date, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out _);
        return isParsable;
    }
}