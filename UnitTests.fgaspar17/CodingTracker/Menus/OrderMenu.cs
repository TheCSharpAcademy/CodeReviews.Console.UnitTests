using Spectre.Console;

namespace CodingTracker;

public enum OrderOptions
{
    [Title("Quit")]
    Quit,
    [Title("Ascending")]
    Asc,
    [Title("Descending")]
    Desc,
}
public class OrderMenu : IMenu<OrderOptions>
{
    public IPrompt<OrderOptions> GetMenu()
    {
        return new SelectionPrompt<OrderOptions>()
            .Title("Choose an option: ")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
            .AddChoices(Enum.GetValues<OrderOptions>()).UseConverter<OrderOptions>(EnumHelper.GetTitle);
    }
}
