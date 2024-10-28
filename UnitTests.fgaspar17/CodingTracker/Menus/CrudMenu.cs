using Spectre.Console;

namespace CodingTracker;

public enum CrudMenuOptions 
{
    [Title("Quit")]
    Quit,
    [Title("Create a coding session")]
    Create,
    [Title("Update a coding session")]
    Update,
    [Title("Delete a coding session")]
    Delete,
    [Title("Show coding sessions")]
    Show,
    [Title("Start Stopwatch")]
    Stopwatch,
    [Title("Reports")]
    Reports,
    [Title("Manage Goals")]
    Goals, 
}

public class CrudMenu : IMenu<CrudMenuOptions>
{
    public IPrompt<CrudMenuOptions> GetMenu()
    {
        return new SelectionPrompt<CrudMenuOptions>()
            .Title("Choose an option: ")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(Enum.GetValues<CrudMenuOptions>()).UseConverter<CrudMenuOptions>(EnumHelper.GetTitle);
    }
}
