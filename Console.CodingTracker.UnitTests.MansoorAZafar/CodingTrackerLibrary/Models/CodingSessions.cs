namespace CodingTrackerLibrary.Models;

internal class CodingSession
{
    public static string[] headers = { "Id", "StartDate", "EndDate", "Duration", "Units" };
    public int? Id { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public float? Duration { get; set; }
    public string? Units { get; set; } 

    public CodingSession(int id, string startDate, string endDate, float duration)
    {
        this.Id = id; 
        this.StartDate = startDate; 
        this.EndDate = endDate;
        this.Duration = duration;
    }

    public CodingSession(int id, string startDate, string endDate, float duration, string units)
    {
        this.Id = id;
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.Duration = duration;
        this.Units = units;
    }

    public CodingSession(string startDate, string endDate, float duration)
    {
        this.StartDate = startDate;
        this.EndDate = endDate;
        this.Duration = duration;
    }

    public CodingSession(string startDate)
    {
        this.StartDate = startDate;
    }

    public CodingSession(int id)
    {
        this.Id = id;
    }

    public CodingSession() { }

    public override bool Equals(object? obj)
    {
        if (obj is not CodingSession other) return false;

        return (this.Id == other.Id
            && this.StartDate == other.StartDate
            && this.EndDate == other.EndDate
            && this.Duration == other.Duration
            && this.Units == other.Units);
    }

    public override int GetHashCode()
        => HashCode.Combine(Id, StartDate, EndDate, Duration, Units);
}