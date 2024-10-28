using Spectre.Console;

namespace CodingTracker;

public class StartTimeMenu : IMenu<string>
{
    public IPrompt<string> GetMenu()
    {
        return new TextPrompt<string>("Introduce a Start Time format [blue](yyyy-MM-dd HH:mm)[/]: ")
            .PromptStyle("yellow")
            .ValidationErrorMessage("[red]The input must be a valid date in the specified format![/]")
            .Validate(input => new DateTimeValidator().Validate(input))
            .DefaultValue(DateTime.Now.ToString("yyyy-MM-dd HH:mm"))
            .ShowDefaultValue(true);
            
    }
}
