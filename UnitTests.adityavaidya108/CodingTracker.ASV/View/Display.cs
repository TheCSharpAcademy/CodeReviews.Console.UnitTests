using Spectre.Console;

namespace CodingTracker;

public class Display
{
    public static void DisplayMenu()
    {
        var table = new Table();
        AnsiConsole.Markup("[orange1]Hello! What would you like to do?[/]\n");
        table.AddColumn(new TableColumn("[orange1]Option[/]").Centered());
        table.AddColumn(new TableColumn("[orange1]Description[/]").Centered());
        table.AddRow("[orange1]0[/]", "[orange1]View Record[/]");
        table.AddRow("[orange1]1[/]", "[orange1]Insert Record[/]");
        table.AddRow("[orange1]2[/]", "[orange1]Delete Record[/]");
        table.AddRow("[orange1]3[/]", "[orange1]Update Record[/]");
        table.AddRow("[orange1]4[/]", "[orange1]Exit Application[/]");
        table.Border = TableBorder.Rounded;
        table.Centered();
        AnsiConsole.Render(table);
    }

    public static void GetCodingSessionStartTimeConsoleMessage()
    {
        AnsiConsole.Markup("[yellow3]Enter valid Start-time in the format HH:mm (e.g., 12:30)\n[/]");
    }

    public static void GetCodingSessionEndTimeConsoleMessage()
    {
        AnsiConsole.Markup("[yellow3]Enter valid End-time in the format HH:mm (e.g., 12:45)\n[/]");
    }

}