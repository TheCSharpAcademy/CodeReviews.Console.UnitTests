using CodingTracker.kwm0304.Enums;

namespace CodingTracker.kwm0304.Utils;

public class Validator
{
    public static string ConvertDateTimeToString(DateTime time)
    {
        return time.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static int ConvertTimeToInt(TimeSpan time)
    {
        return (int)time.TotalSeconds;
    }
    public static DateTime ConvertTextToDateTime(string dateStr)
    {
        return DateTime.ParseExact(dateStr, "yyyy-MM-dd HH:mm:ss", null);
    }

    public static int ToDays(DateRange range)
    {
        return range switch
        {
            DateRange.Week => 7,
            DateRange.Month => 30,
            DateRange.Year => 365,
            _ => throw new ArgumentOutOfRangeException(nameof(range), $"Unexpected DateRange value: {range}")
        };
    }

    public static DateRange ToDateRange(int range)
    {
        return range switch
        {
            7 => DateRange.Week,
            30 => DateRange.Month,
            365 => DateRange.Year,
            _ => throw new ArgumentOutOfRangeException(nameof(range), $"Unexpected DateRange value: {range}")
        };
    }
}
