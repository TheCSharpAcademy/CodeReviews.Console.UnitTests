using Spectre.Console;

namespace CodingTracker;

public class GoalsEndTimeMenu : IMenu<string>
{
    private readonly DateTime _start;

    public GoalsEndTimeMenu(DateTime start)
    {
        _start = start;
    }

    public IPrompt<string> GetMenu()
    {
        DateTimeValidator dateTimeValidator = new DateTimeValidator();

        return new TextPrompt<string>("Introduce a End Time format [blue](yyyy-MM-dd HH:mm)[/] and higher than the Start Time: ")
            .PromptStyle("yellow")
            .ValidationErrorMessage("[red]The input must be a valid date in the specified format![/]")
            .Validate(input => dateTimeValidator.FutureValidate(input, _start))
            .DefaultValue(DateTime.Today.AddMonths(3).ToString("yyyy-MM-dd HH:mm"))
            .ShowDefaultValue(true);
    }
}
