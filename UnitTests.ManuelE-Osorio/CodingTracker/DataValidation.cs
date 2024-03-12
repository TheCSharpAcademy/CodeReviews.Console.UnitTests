using System.Globalization;

namespace CodingTracker;

public class DataValidation
{
    public static bool ValidateInteger(string? input)
    {
        bool validInput;

        validInput = int.TryParse(input, out int validInputInt);
        return validInput;
    }

    public static bool ValidateInteger(string? input, int minvalue, int maxvalue)
    {
        bool validInput;

        validInput = int.TryParse(input, out int validInputInt);
        if(validInput)
        {
            if(validInputInt<minvalue || validInputInt > maxvalue)
            {
                validInput = false;
            }
        }
        return validInput;
    }

    public static bool ValidateTime(string? startTime)
    {
        bool validInput;

        validInput = TimeOnly.TryParseExact(startTime,"HH:mm", out TimeOnly validStartTime);
        return validInput;
    }

    public static bool ValidateTime(string? startDate, string? startTime, string? endDate, string? endTime)
    {
        bool validInput = false;
 
        var validStartDate = DateTime.TryParseExact(startDate+" "+startTime,"yyyy/MM/dd HH:mm",CultureInfo.InvariantCulture,
        DateTimeStyles.None ,out DateTime validStartTime);
        var validEndDate = DateTime.TryParseExact(endDate+" "+endTime,"yyyy/MM/dd HH:mm",CultureInfo.InvariantCulture,
        DateTimeStyles.None, out DateTime validEndTime);
        
        if(validStartDate && validEndDate)
            validInput = validEndTime > validStartTime;
        return validInput;
    }

    public static bool ValidateDate(string? startDate)
    {
        bool validInput;
        
        validInput = DateOnly.TryParseExact(startDate, "yyyy/MM/dd",CultureInfo.InvariantCulture, 
        DateTimeStyles.None, out DateOnly validStartDate);
        return validInput;
    }

    public static bool ValidateDate(string? startDate, string? endDate)
    {
        bool validInput = false;
        
        var isStartDateValid = DateOnly.TryParseExact(startDate, "yyyy/MM/dd",CultureInfo.InvariantCulture, 
        DateTimeStyles.None, out DateOnly validStartDate);
        var isEndDateValid = DateOnly.TryParseExact(endDate, "yyyy/MM/dd",CultureInfo.InvariantCulture, 
        DateTimeStyles.None, out DateOnly validEndDate);
        
        if(isStartDateValid && isEndDateValid)
            validInput = validEndDate >= validStartDate;
        return validInput;
    }

    public static bool ValidateYesNoQuestion(string? input)
    {
        bool validInput = false;

        if (input == "y" || input == "n")
        {
            validInput = true;
        }
        return validInput;
    }

    public static bool ValidateTotalHours(string? input)
    {
        bool validInput;
        validInput = TimeSpan.TryParseExact(input,"d\\.hh\\:mm",CultureInfo.InvariantCulture, out TimeSpan inputTime);
        return validInput;
    }
}