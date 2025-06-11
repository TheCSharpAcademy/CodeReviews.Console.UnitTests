namespace CodingTracker
{
    /// <summary>
    /// Represents database record for CodingSession
    /// </summary>
    public class CodingSession
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public TimeSpan Duration
        {
            get => End - Start;
        }
        
    }
}