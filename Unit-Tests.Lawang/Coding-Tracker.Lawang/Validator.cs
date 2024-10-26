using System;
using System.Globalization;

namespace Lawang.Coding_Tracker;

public static class Validator
{
    public static bool ValidateUserTime(string input)
    {
        if(input == "0")
        {
            throw new ExitOutOfOperationException("");
        }

        DateTime time;

        if(DateTime.TryParseExact(input, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
        {
            return true;
        }
        return false;
    }

    public static bool ValidateTimeDuration(string input)
    {
        if(input == "0")
        {
            throw new ExitOutOfOperationException("");
        }

        TimeSpan time;

        if(TimeSpan.TryParseExact(input, "c", CultureInfo.InvariantCulture, out time))
        {
            return true;
        }
        return false;
    }

    public static bool ValidateDate(string date)
    {
        if(date == "0")
        {
            throw new ExitOutOfOperationException("");
        }

        DateTime validDate;

        if(DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out validDate))
        {
            return true;
        }
        return false;
    }
}
