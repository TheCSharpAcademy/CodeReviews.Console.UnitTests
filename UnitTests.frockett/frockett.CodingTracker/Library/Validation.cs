
using System.Globalization;
using System;
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

    public static (bool, DateTime) IsValidDateTime(string sDateTime)
    {
        string validFormat = "dd-MM-yyyy HH:mm";
        return (DateTime.TryParseExact(sDateTime, validFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime), dateTime);
    }
}
