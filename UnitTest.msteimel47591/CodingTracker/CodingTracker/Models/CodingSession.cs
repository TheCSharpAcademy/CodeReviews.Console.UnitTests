
namespace CodingTracker.Models
{
    public class CodingSession
    {
        public int Id { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Focus { get; set; }
        public string Duration { get; private set; }

        public string CalculateDuration()
        {
            TimeSpan timeSpan = Helper.ConvertStringToDateTime(this.EndTime) - Helper.ConvertStringToDateTime(this.StartTime);
            return this.Duration = timeSpan.ToString();
        }
    }
}
