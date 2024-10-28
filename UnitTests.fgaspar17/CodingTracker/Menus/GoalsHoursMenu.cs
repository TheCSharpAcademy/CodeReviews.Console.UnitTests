using Spectre.Console;

namespace CodingTracker;

public class GoalsHoursMenu : IMenu<int>
{
    public IPrompt<int> GetMenu()
    {

        return new TextPrompt<int>("Introduce a number of Hours for the goal:")
            .PromptStyle("bold yellow")
            .ValidationErrorMessage("[red]Invalid input. Please enter a valid integer.[/]")
            .DefaultValue(180);
    }
}
