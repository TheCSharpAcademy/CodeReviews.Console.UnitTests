using Spectre.Console;

namespace CodingTracker;

public interface IMenu<T>
{
    IPrompt<T> GetMenu();
}
