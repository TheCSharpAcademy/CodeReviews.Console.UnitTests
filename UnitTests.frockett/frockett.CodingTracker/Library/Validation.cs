
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library;

public static class Validation
{
    public static bool IsValidEndAndStartTime(DateTime startTime, DateTime endTime)
    {
        return endTime > startTime;
    }

    public static (bool, DateOnly) IsValidMonthAndYear(string sDate)
    {
        return (DateOnly.TryParseExact(sDate, "MM-yyyy", out DateOnly date), date);
    }
}
