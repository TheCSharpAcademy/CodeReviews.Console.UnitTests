
using Spectre.Console;

namespace CodingTracker.KamilKolanowski.Services;

public class Validation
{
    public ValidationResult ValidateStartTime(DateTime input)
    {
        if (input.TimeOfDay == TimeSpan.Zero)
            return ValidationResult.Error("[red]Time of day is required![/]");
        if (input > DateTime.MaxValue) 
            return ValidationResult.Error("[red]Date can't be greater than: [/][yellow]9999-12-31[/]");
        if (input <= new DateTime(1753, 1, 1))
            return ValidationResult.Error($"[red]Date can't be lower than: [/][yellow]{new DateTime(1753, 1, 1)}[/]");
        return ValidationResult.Success();
    }
    
    public ValidationResult ValidateEndTime(DateTime input, DateTime userSessionStartTime)
    {
        if (input.TimeOfDay == TimeSpan.Zero) 
            return ValidationResult.Error("[red]Time of day is required![/]");
        if (input <= userSessionStartTime) 
            return ValidationResult.Error("[red]The end date of your session can't be before the start date![/]");
        if (input <= new DateTime(1753, 1, 1)) 
            return ValidationResult.Error($"[red]Date can't be lower than: [/][yellow]{new DateTime(1753, 1, 1)}[/]");
        return ValidationResult.Success();
    }
}