using System.Globalization;

namespace AnaClos.CodingTracker;

public class CodingSession
{
    public int Id {  get; set; }
    public string StartTime {  get; set; }
    public string EndTime { get; set; }
    public TimeSpan Duration { get; set; }

    public DateTime String2DateTime(string stringTime)
    {
        DateTime dateTime =  DateTime.ParseExact(stringTime, "dd/MM/yy HH:mm:ss", new CultureInfo("en-US"));
        return dateTime;
    }

    public void CalculateDuration()
    {
        Duration = CalculateTimeSpan(String2DateTime(StartTime), String2DateTime(EndTime));
    }

    public TimeSpan CalculateTimeSpan(DateTime start, DateTime end)
    {
        return end - start;
    }

    public override string ToString()
    {
        string stringDuration = Duration.ToString(@"dd hh\:mm\:ss");
        return $"Id: {Id} Start Time: {StartTime} End Time: {EndTime} Duration: {stringDuration}";
    }
}