using CodingTrackerLibrary;
using Spectre.Console;

namespace CodingTracker;

public class IdValidator : IValidator<int>
{
    public ValidationResult Validate(int input)
    {
        if (CodingSessionController.GetCodingSessionById(input, Settings.GetConnectionString()) == null)
        {
            return ValidationResult.Error("[red]The ID must exist.[/]");
        }
        return ValidationResult.Success();
    }
}
