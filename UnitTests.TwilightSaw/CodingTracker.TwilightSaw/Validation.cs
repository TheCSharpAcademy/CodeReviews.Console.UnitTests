using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace CodingTracker.TwilightSaw;

public class Validation
{
    public bool IsExecutable(Action action)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            AnsiConsole.Write(new Rows(new Text($"\n{e.Message}", new Style(Color.Red))));
            return false;
        }
        return true;
    }

    public Exception? CheckWithMessage(Action action, string message)
    {
        try
        {
            action();
        }
        catch (Exception e)
        {
            Console.WriteLine(message);
            return e;
        }
        return null;
    }
}