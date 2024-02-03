using System.Globalization;

namespace Doc415.CodingTracker;

public class Validator
{
    public static (bool,DateTime) IsValidDate(String date)
    {
        DateTime startDate;
        var valid = DateTime.TryParseExact(date, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate);
        return (valid,startDate);
    }

    public static (bool,int) IsValidHour(string hour)
    {
        int parsed = 0;
        var valid = int.TryParse(hour, out parsed);
        if(!valid || parsed <= 0) 
        {
            return (false,parsed);
            Console.WriteLine("Goal cant be 0 or negative hours.");
        } 
        else
            return (true,parsed);
    }
}
