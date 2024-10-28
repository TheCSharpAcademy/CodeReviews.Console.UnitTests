using CodingTrackerLibrary;
using Spectre.Console;

namespace CodingTracker;

public class GoalIdValidator : IValidator<int>
{
    public ValidationResult Validate(int input)
    {
        if (CodingGoalController.GetCodingGoalById(input, Settings.GetConnectionString()) == null)
        {
            return ValidationResult.Error("[red]The ID must exist.[/]");
        }
        return ValidationResult.Success();
    }
}
