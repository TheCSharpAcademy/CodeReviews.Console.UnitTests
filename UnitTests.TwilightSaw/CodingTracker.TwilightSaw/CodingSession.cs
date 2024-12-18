namespace CodingTracker.TwilightSaw;

public struct CodingSession
{
    private int Id;
    public string Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; }

    public DateTime GetStartTime()
    {
        if (StartTime > EndTime)
            throw new Exception("You are trying to add session that finishes in another day or entered bad Start Time or End Time");
        return StartTime;
    }

    public string CalculateDuration()
    {
        TimeSpan.TryParse(EndTime.ToLongTimeString(), out var endTime);
        TimeSpan.TryParse(StartTime.ToLongTimeString(), out var startTime);
        return (endTime - startTime).ToString();
    }

    public override string ToString()
    {
        return $"{Date} {StartTime.ToLongTimeString()} {EndTime.ToLongTimeString()} {Duration}" ;
    }
}