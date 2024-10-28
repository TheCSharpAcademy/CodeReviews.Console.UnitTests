using Spectre.Console;

namespace CodingTracker;

public static class DisplayMenu
{
    public static T ShowMenu<T>(IMenu<T> menu)
    {
        return AnsiConsole.Prompt<T>(menu.GetMenu());
    }
}
