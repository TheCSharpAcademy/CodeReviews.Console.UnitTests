namespace CodingLogger.Models
{
    public class CodingSession
    {
        public int Id { get; set; }
        public long Duration { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CodingSession(int id, long duration, DateTime startTime, DateTime endTime)
        {
            Id = id;
            Duration = duration;
            StartTime = startTime;
            EndTime = endTime;
        }

        protected CodingSession()
        {

        }
        public string GetFormattedDuration()
        {
            var timespan = TimeSpan.FromTicks(Duration);
            return $"{timespan.Days}d {timespan.Hours}h {timespan.Minutes}m {timespan.Seconds}s";
        }
    }
}

