namespace CodingTracker.Models
{
    internal class CodingSession
    {
        internal string? Project { get; set; }
        internal string? StartDate { get; set; }
        internal string? EndDate { get; set; }
        internal string? StartTime { get; set; }
        internal string? EndTime { get; set; }
        internal string? Duration { get; set; }
        internal int Rowid { get; set; }
        internal string? DurationCount { get; set; }
        internal string? TotalDuration { get; set; }
        internal string? Average { get; set; }
    }
}