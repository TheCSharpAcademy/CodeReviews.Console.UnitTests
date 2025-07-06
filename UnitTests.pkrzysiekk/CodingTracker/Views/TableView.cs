using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Views;

public static class TableView
{
    public static void ShowTable(List<CodingSession> list)
    {
        var table = CreateTable(list);

        AnsiConsole.Write(table);
    }

    public static Table CreateTable(List<CodingSession> list)
    {
        var rowCount = 0;
        var table = new Table();
        table.AddColumns("Id", "Start Date", "End Date", "Duration [[mins]]");
        foreach (var c in list)
        {
            if (rowCount % 2 == 0)
                table.AddRow($"{c.Id}", $"{c.StartDate}", $"{c.EndDate}", $"{c.Duration}");
            else
                table.AddRow($" [gray]{c.Id}[/]", $"[gray]{c.StartDate}[/]", $"[gray]{c.EndDate}[/]",
                    $"[gray]{c.Duration}[/]");

            rowCount++;
        }

        return table;
    }
}