namespace CodingTracker;

public static class DataValidation
{
    public static bool IsPositiveNumber(string input)
    {
        return int.TryParse(input, out _) && int.Parse(input) >= 0;
    }

    public static bool IsValidDate(string input)
    {
        return DateTime.TryParseExact(input, "d-M-yy", System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out _);
    }

    public static bool IsValidTime(string input)
    {
        return DateTime.TryParseExact(input, "H:mm", System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out _);
    }
}