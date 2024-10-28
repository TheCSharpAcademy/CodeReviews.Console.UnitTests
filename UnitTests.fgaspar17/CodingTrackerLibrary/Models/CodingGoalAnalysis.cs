namespace CodingTrackerLibrary;

public class CodingGoalAnalysis
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Hours { get; set; }
    public int HoursAway { get; set; }
    public int HoursPerDay { get; set; }
}
