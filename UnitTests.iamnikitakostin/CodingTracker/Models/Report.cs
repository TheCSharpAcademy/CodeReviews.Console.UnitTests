namespace CodingTracker.Models
{
    public class WeekReportModel
    {
        public int Year { get; set; }
        public int WeekNumber { get; set; }
        public string WeekStart { get; set; }
        public string WeekEnd { get; set; }
        public int TotalDuration { get; set; }
        public int Count { get; set; }
    }

    public class MonthReportModel
    {
        public string Month { get; set; }
        public int Count { get; set; }
        public int TotalDuration { get; set; }
    }

    public class YearReportModel
    {
        public int Year { get; set; }
        public int Count { get; set; }
        public int TotalDuration { get; set; }
    }
}
