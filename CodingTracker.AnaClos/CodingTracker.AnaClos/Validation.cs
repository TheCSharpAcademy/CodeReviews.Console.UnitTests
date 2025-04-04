using System.Globalization;

namespace AnaClos.CodingTracker;

public class Validation
{
    public bool Validate2Dates(string startTime,string endTime)
    {
        var codingSession = new CodingSession { StartTime = startTime, EndTime = endTime };
        codingSession.CalculateDuration();
        return codingSession.Duration.TotalSeconds > 0;
    }

    public bool ValidateDate(string date)
    {
        bool ok;
        DateTime dateTime = DateTime.MinValue;

        if (date == "r")
        {
            return true;
        }

        ok= DateTime.TryParseExact(
            date, 
            "dd/MM/yy HH:mm:ss", 
            new CultureInfo("en-US"),
            DateTimeStyles.None,
            out dateTime);        
        return ok;
    }

    public bool ValidateInt(string response)
    {
        bool ok;
        int number;

        if (response == "r")
        {
            return true;
        }

        ok = Int32.TryParse(response, out number);       
        return ok;
    }
}