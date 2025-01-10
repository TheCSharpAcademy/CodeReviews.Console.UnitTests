using Spectre.Console;

namespace CodingTracker;

public class Validation
{
    //Returns true only if the time is in "HH:mm" format
    public static bool TryParseTime(string input, out TimeOnly time)
    {
        time = default;

        if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
        {
            time = TimeOnly.FromDateTime(parsedTime);
            return true;
        }
        return false;
    }

    //Returns true only if the date is in "yyyy-MM-dd" format and not from the future
    public static bool TryParseDate(string input, out DateOnly date)
    {
        date = default;

        if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
        {
            if (parsedDate <= DateTime.Now.Date)
            {
                date = DateOnly.FromDateTime(parsedDate);
                return true;
            }
        }
        return false;
    }

    //Calculates Time Span between two TimeOnly variables
    public static TimeSpan CalculateTimeSpan(TimeOnly startSessionTime, TimeOnly endSessionTime)
    {
        DateTime startDateTime = DateTime.Today.Add(startSessionTime.ToTimeSpan());
        DateTime endDateTime = DateTime.Today.Add(endSessionTime.ToTimeSpan());
        TimeSpan duration = endDateTime - startDateTime;
        duration = new TimeSpan(duration.Days, duration.Hours, duration.Minutes, duration.Seconds);
        return duration;
    }

    //Returns true if endTime is a valid TimeOnly in format "HH:mm" and greater than startTime
    public static bool ValidateEndSessionTime(string endTime, string startTime)
    {
        if (TryParseTime(endTime, out TimeOnly Time) && TryParseTime(startTime, out TimeOnly Time1))
        {
            if (Time > Time1)
                return true;
        }
        return false;
    }
}