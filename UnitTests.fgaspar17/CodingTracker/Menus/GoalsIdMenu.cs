using Spectre.Console;

namespace CodingTracker;

public class GoalsIdMenu : IMenu<int>
{
    public IPrompt<int> GetMenu()
    {

        return new TextPrompt<int>("Introduce an ID:")
            .PromptStyle("bold yellow")
            .ValidationErrorMessage("[red]Invalid input. Please enter a valid integer.[/]")
            .Validate(id => new GoalIdValidator().Validate(id));
    }
}
