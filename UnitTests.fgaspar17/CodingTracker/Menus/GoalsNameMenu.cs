using Spectre.Console;

namespace CodingTracker;

public class GoalsNameMenu : IMenu<string>
{
    public IPrompt<string> GetMenu()
    {

        return new TextPrompt<string>("Introduce a Name for the Goal:")
            .PromptStyle("bold yellow")
            .ValidationErrorMessage("[red]Invalid input. Please enter a valid string.[/]");
    }
}
