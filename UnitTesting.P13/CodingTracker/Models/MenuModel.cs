namespace CodingTracker.Models;

internal class MenuModel
{
    internal string? Option { get; set; }
    internal string? Project { get; set; }

    internal bool IsCodingSessionRunning { get; set; }
    internal string? CurrentCodingSession { get; set; }
    internal string? StartDate { get; set; }
    internal string? EndDate { get; set; }
    internal string? StartTime { get; set; }
    internal string? EndTime { get; set; }
    internal string? Duration { get; set; }
    internal string? SqlCommandText { get; set; }
    internal CodingSession? CurrentData { get; set; }
}