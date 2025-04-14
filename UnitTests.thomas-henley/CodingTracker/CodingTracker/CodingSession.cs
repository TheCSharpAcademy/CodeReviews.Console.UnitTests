using System.Globalization;

namespace CodingTracker;

public class CodingSession
{
    public int Id { get; set; }
    public string StartTime { get; set; } = "";
    public string EndTime { get; set; } = "";
    public long Duration { get; set; }

    public CodingSession()
    {
    }

    public CodingSession(string start, string end)
    {
        StartTime = start;
        EndTime = end;
        Duration = CalculateDurationMinutes();
    }

    public CodingSession(DateTime start, DateTime end)
    {
        StartTime = start.ToString("yyyy-MM-dd HH:mm");
        EndTime = end.ToString("yyyy-MM-dd HH:mm");
        Duration = CalculateDurationMinutes();
    }

    public DateTime GetStartDateTime()
    {
        return DateTime.ParseExact(StartTime, "yyyy-MM-dd HH:mm", new CultureInfo("en-US"));
    }

    public DateTime GetEndDateTime()
    {
        return DateTime.ParseExact(EndTime, "yyyy-MM-dd HH:mm", new CultureInfo("en-US"));
    }

    public TimeSpan CalculateDurationTimeSpan()
    {
        return GetEndDateTime() - GetStartDateTime();
    }

    public long CalculateDurationMinutes()
    {
        return (long)CalculateDurationTimeSpan().TotalMinutes;
    }

    public void SaveDuration()
    {
        Duration = CalculateDurationMinutes();
    }
}
