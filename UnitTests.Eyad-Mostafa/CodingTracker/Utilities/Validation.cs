using System.Globalization;

namespace CodingTracker.Utilities;

public class Validation
{
    public static bool IsValidDuration(int duration)
    {
        return duration > 0;
    }

    public static bool IsValidTime(string? time)
    {
        return !string.IsNullOrEmpty(time) && TimeOnly.TryParse(time, out _);
    }

    public static bool IsValidDate(string? dateInput)
    {
        return DateTime.TryParseExact(dateInput, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _);
    }

    public static bool IsValidNumber(string? value)
    {
        return !String.IsNullOrEmpty(value) && int.TryParse(value, out _);
    }
}
