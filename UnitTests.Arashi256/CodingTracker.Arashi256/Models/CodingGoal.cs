
namespace CodingTracker.Arashi256.Models
{
    public class CodingGoal
    {
        public int? Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Hours { get; set; }
        public DateTime DeadlineDateTime { get; set; }
    }
}
