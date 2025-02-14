using CodingTracker.Data;

namespace CodingTracker
{
    internal class GenerateReport
    {
        static internal void ByWeeks()
        {
            DbConnection.GetSessionsByPeriod("week");
        }

        static internal void ByMonths()
        {
            DbConnection.GetSessionsByPeriod("month");
        }

        static internal void ByYears() {
            DbConnection.GetSessionsByPeriod("year");
        }
    }
}
