namespace CodingTracker.Models
{
    public class Goal
    {
        internal int Id { get; set; }
        internal int PeriodInDays { get; set; }
        internal string StartDate { get; set; }
        internal int DesiredLengthInSeconds { get; set; }
        internal bool IsActive { get; set; }
    }
}
