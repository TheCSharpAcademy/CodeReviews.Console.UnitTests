namespace cacheMe512.CodeTracker.Models;

public class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int DurationInSeconds { get; set; }
    public TimeSpan Duration => TimeSpan.FromSeconds(DurationInSeconds);

    public TimeSpan CalculateDuration() => (EndTime - StartTime);

}
