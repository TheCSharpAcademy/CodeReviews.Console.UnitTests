using System;
using System.Globalization;
using System.Linq;

namespace CodingTracker.SamGannon.Utility;

public class Validation
{
    public bool IsValidTimeFormat(string time)
    {
        bool isValid = TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _);
        if (!isValid)
        {
            return false;
        }

        return isValid;
    }

    public bool IsValidDate(string date)
    {
        bool isValid = DateTime.TryParseExact(date, "MM-dd-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _);
        if (!isValid)
        {
            return false;
        }

        return isValid;
    } 

    public bool IsValidId(string commandInput)
    {
        if (int.TryParse(commandInput, out int result))
        {
            return result >= 0;
        }

        return false;
    }
}
