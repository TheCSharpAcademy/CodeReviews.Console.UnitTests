using System.Globalization;

namespace CodingTracker;

public static class Validation
{
    public static bool ValidateDateTimeInput(string dateInput)
    {
        if (DateTime.TryParseExact(dateInput, "MM/dd/yy hh:mm tt", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateFocusInput(string focusInput)
    {
        if (string.IsNullOrWhiteSpace(focusInput))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static int ValidateSessionIdInput(string idInput)
    {
        if (int.TryParse(idInput, out int id) && id > 0)
        {
            return id;
        }
        else
        {
            return -1;
        }
    }

    public static bool ValidateDateInput(string dateInput)
    {
        if (DateTime.TryParseExact(dateInput, "MM/dd/yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateMonthAndYearInput(string dateInput)
    {
        if (DateTime.TryParseExact(dateInput, "MM/yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool ValidateYearInput(string dateInput)
    {
        if (DateTime.TryParseExact(dateInput, "yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
