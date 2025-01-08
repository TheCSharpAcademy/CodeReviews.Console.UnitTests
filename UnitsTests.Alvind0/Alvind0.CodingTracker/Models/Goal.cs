namespace Alvind0.CodingTracker.Models;
public class Goal
{
    public int Id { get; set; }
    public string Name { get; set; } = "Undefined";
    public TimeSpan DurationGoal { get; set; }
    public string DurationGoalString { get; set; } = "00:00";
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan Progress { get; set; } = TimeSpan.Zero;
    public string ProgressString { get; set; } = "00:00";
    public bool IsCompleted => Progress >= DurationGoal;

}