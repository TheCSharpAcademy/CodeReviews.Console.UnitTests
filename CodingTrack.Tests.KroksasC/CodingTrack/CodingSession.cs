namespace CodingTrack
{
    public class CodingSession
    {
        private int Id { get; set; }
        private DateTime StartTime { get; set; }
        private DateTime EndTime { get; set; }
        private DateTime Duration { get; set; }

        public CodingSession(int id, DateTime startTime, DateTime endTime, DateTime duration)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
        }

        public CodingSession() { }

        public int GetId() { return Id; }
        public DateTime GetStartTime() { return StartTime; }
        public DateTime GetEndTime() { return EndTime; }
        public TimeSpan GetDuration() { return EndTime - StartTime; }

    }
}
