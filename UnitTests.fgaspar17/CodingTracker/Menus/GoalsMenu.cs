using Spectre.Console;

namespace CodingTracker;

public enum GoalsMenuOptions 
{
    [Title("Quit")]
    Quit,
    [Title("Create a coding goal")]
    Create,
    [Title("Update a coding goal")]
    Update,
    [Title("Delete a coding goal")]
    Delete,
    [Title("Show coding goals")]
    Show,
    [Title("Hours to reach a goal")]
    HoursNeeded,
}

public class GoalsMenu : IMenu<GoalsMenuOptions>
{
    public IPrompt<GoalsMenuOptions> GetMenu()
    {
        return new SelectionPrompt<GoalsMenuOptions>()
            .Title("Choose an option: ")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(Enum.GetValues<GoalsMenuOptions>()).UseConverter<GoalsMenuOptions>(EnumHelper.GetTitle);
    }
}