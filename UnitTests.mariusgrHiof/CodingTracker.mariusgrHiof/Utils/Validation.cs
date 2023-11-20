using System.Globalization;

namespace CodingTracker.Utils;

public static class Validation
{
    public static bool ValidateDate(string date)
    {
        return DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", new CultureInfo("nb-NO"), DateTimeStyles.None, out _);
    }

    public static bool ValidateDateRange(string DateStart, string DateEnd)
    {
        if (!ValidateDate(DateStart) || !ValidateDate(DateEnd))
        {
            return false;
        }

        TimeSpan timeSpan = DateTime.Parse(DateEnd) - DateTime.Parse(DateStart);

        return timeSpan.Ticks > 0;

    }

    public static bool ValidateId(string id)
    {
        bool result = false;
        try
        {
            result = Convert.ToInt32(id) > 0;
            return result;
        }
        catch (Exception)
        {

            Console.WriteLine("invalid id");
            result = false;
        }

        return result;
    }
}

