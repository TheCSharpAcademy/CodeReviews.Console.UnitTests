using Spectre.Console;

namespace cacheMe512.CodeTracker;

public interface IConsoleWrapper
{
    string AskString(string message);
    int AskInt(string message);
}

public class ConsoleWrapper : IConsoleWrapper
{
    public string AskString(string message) => AnsiConsole.Ask<string>(message);
    public int AskInt(string message) => AnsiConsole.Ask<int>(message);
}
