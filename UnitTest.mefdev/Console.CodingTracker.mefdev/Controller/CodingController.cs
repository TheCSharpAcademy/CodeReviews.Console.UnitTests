using CodingLogger.Models;
using Spectre.Console;
using Spectre.Console.Cli;

namespace CodingLogger.Controller
{
    public class CodingController
    {
        public CodingController()
        {
        }
        public void DisplayMenu()
        {
            RenderCustomLine("DodgerBlue1", "Coding Logger Menu");
            var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.LightSteelBlue1)
            .AddColumn(new TableColumn("[bold DodgerBlue1]Option[/]").Centered().Width(15)).Width(80)
            .AddColumn(new TableColumn("[bold blue]Description[/]"))
            .AddRow("[bold DodgerBlue1]a [/] ", "- Add a coding session")
            .AddRow("[bold DodgerBlue1]v [/] ", "- View a coding session")
            .AddRow("[bold DodgerBlue1]d [/] ", "- Delete a coding session")
            .AddRow("[bold DodgerBlue1]u [/] ", "- Update a coding session")
            .AddRow("[bold DodgerBlue1]s [/] ", "- View all coding sessions")
            .AddRow("[bold DodgerBlue1]q [/] ", "- Exit");
            AnsiConsole.Write(table.Centered());
        }
        public void RenderCustomLine(string color, string title)
        {
            var rule = new Rule($"[{color}]{title}[/]");
            rule.RuleStyle($"{color} dim");
            AnsiConsole.Write(rule);
        }
        public void DisplayCodingSession(CodingSession session)
        {
            var table = new Table()
           .Border(TableBorder.Rounded)
           .BorderColor(Color.LightSteelBlue1)
           .AddColumn(new TableColumn("[bold DodgerBlue1]Property[/]").Centered())
           .AddColumn(new TableColumn("[bold blue]Value[/]"));

            table.AddRow("ID", session.Id.ToString())
                 .AddRow("Start Time", session.StartTime.ToString("yyyy-MM-dd HH:mm"))
                 .AddRow("End Time", session.EndTime.ToString("yyyy-MM-dd HH:mm"))
                 .AddRow("Duration", session.GetFormattedDuration());

            AnsiConsole.Write(table.Centered());
        }
        public void DisplayCodingSessions(List<CodingSession> sessions)
        {
            var table = new Table()
            .AddColumn(new TableColumn("[bold blue]ID[/]").Centered())
            .AddColumn(new TableColumn("[bold blue]Start Time[/]"))
            .AddColumn(new TableColumn("[bold blue]End Time[/]"))
            .AddColumn(new TableColumn("[bold blue]Duration[/]"));

            foreach (var session in sessions)
            {
                table.AddRow(
                    session.Id.ToString(),
                    session.StartTime.ToString("yyyy-MM-dd HH:mm"),
                    session.EndTime.ToString("yyyy-MM-dd HH:mm"),
                    session.GetFormattedDuration()
                );
            }
            AnsiConsole.Write(table.Centered());
        }

    }
}

