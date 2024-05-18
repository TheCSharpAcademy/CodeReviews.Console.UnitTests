using Spectre.Console;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTrackerV2.Helpers;

public class Validation
{
    //ask date != validate int
   

    public static bool IsValidInt(string input)
    {
        int result;
        return int.TryParse(input, out result);
    }

    public static bool IsPositiveInt(string input)
    {
        return int.Parse(input) > 0;
    }

   

    public static DateTime ValidateStartDate(string input)
    {
        DateTime date;

        while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) //IsValidDateTime
        {
            //só os returns podem ser validados
            input = AnsiConsole.Ask<string>("\n Invalid date...");
        }
        return date;
    }

    public static DateTime ValidateEndDate(DateTime startDate, string endDateInput)
    {
        DateTime endDate;
        while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
        {
            endDateInput = AnsiConsole.Ask<string>("\nInvalid date...");
        }

        while (startDate > endDate) //Is valid EndDate
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nEnd date can't be before start date...");

            while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date...");
            }
        }
        return endDate;
    }
}
