namespace CodingTracker.KamilKolanowski.Services;

public class SessionsService
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }

    public decimal GetDuration(DateTime startTime, DateTime endTime)
    {
        TimeSpan duration = endTime - startTime;
        return (decimal)duration.TotalSeconds;
    }
}