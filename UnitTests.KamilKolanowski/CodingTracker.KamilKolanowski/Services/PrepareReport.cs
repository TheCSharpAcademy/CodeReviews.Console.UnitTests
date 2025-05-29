using Spectre.Console;
using CodingTracker.KamilKolanowski.Data;
using CodingTracker.KamilKolanowski.Enums;

namespace CodingTracker.KamilKolanowski.Services;

internal class PrepareReport
{
    private readonly DatabaseManager _databaseManager = new();

    internal void PreparePeriodicReport(Options.ReportingOptions reportingOptions, string orderingReport)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[lime]Period[/]");
        table.AddColumn("[lime]Time Spent (s)[/]");
        
        foreach (var row in _databaseManager.CreateReport(reportingOptions, orderingReport))
        {
            table.AddRow($"[dodgerblue1]{row.Period}[/]", $"[dodgerblue1]{row.TimeSpent.ToString()}[/]");
        }
            
        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to go back to the main menu.");
        Console.ReadKey();
    }
}