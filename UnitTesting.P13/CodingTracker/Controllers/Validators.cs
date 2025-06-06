using System.Globalization;

namespace CodingTracker.Controllers;

public class Validators
{
    public static bool ValidateString(string str)
    {
        return str.IndexOfAny(new char[] { '/', '-', '\\', '\'', '"', '(', '[', '{', '?', '!', '&', '>', '<', '=', ',', '.' }) == -1;
    }

    public static bool ValidateDate(string str)
    {
        DateTime date;
        return str.IndexOfAny(new char[] { '/', '\\', '\'', '"', '(', '[', '{', '?', '!', '&', '>', '<', '=', ',', '_' }) == -1 &&
            (DateTime.TryParseExact(str, "yyyy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date));
    }

    public static bool ValidateTime(string str)
    {
        DateTime date;
        return str.IndexOfAny(new char[] { '/', '\\', '\'', '"', '(', '[', '{', '?', '!', '&', '>', '<', '=', ',', '_' }) == -1 &&
            (DateTime.TryParseExact(str, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date));
    }

    public static bool ValidateDurationEstimation(string str)
    {
        TimeSpan timeSpan;
        return str.IndexOfAny(new char[] { '/', '\\', '\'', '"', '(', '[', '{', '?', '!', '&', '>', '<', '=', ',' }) == -1
            && TimeSpan.TryParseExact(str, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None, out timeSpan);
    }

    public static bool ValidateStartAndEnd(string start,string end)
    {
        DateTime startDT = DateTime.Parse(start);
        DateTime endDT = DateTime.Parse(end);
        return startDT < endDT;
    }
}
