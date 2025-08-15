namespace CodingTracker.Models
{
    public class CodingSession
    {
        internal int Id { get; set; }
        internal string StartTime { get; set; }
        internal string? EndTime { get; set; }
        internal int Duration { get; set; }

    }
}