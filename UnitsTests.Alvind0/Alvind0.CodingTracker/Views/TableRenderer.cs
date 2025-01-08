using Alvind0.CodingTracker.Models;
using Spectre.Console;

namespace Alvind0.CodingTracker.Views;

public class TableRenderer
{
    public void RenderGoalsTable(IEnumerable<Goal> goals)
    {
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.AddColumns("Id", "Name", "Goal", "Start Date", "Deadline", "Progress", "Completed", "Hours Needed Per Day");
        table.Columns[0].RightAligned();
        table.Columns[6].LeftAligned();
        foreach (var goal in goals)
        {
            var daysUntilDeadline = (goal.EndDate - DateTime.Today).Days;
            var codingPerDay = (goal.DurationGoal - goal.Progress) / daysUntilDeadline;
            var progress = Convert.ToInt32(goal.Progress / goal.DurationGoal * 100);
            table.AddRow(
                goal.Id.ToString(),
                goal.Name,
                goal.DurationGoal.ToString(@"hh\:mm"),
                goal.StartDate.ToString("MM-dd-yy"),
                goal.EndDate.ToString("MM-dd-yy"),
                $"{progress.ToString()}%",
                goal.IsCompleted ? "Completed" : "Incomplete",
                progress <= 100 ? codingPerDay.ToString(@"hh\:mm") : "Done!");
        }

        AnsiConsole.Clear();
        AnsiConsole.Write(table);
    }

    public void RenderSesionsTable(IEnumerable<CodingSession> sessions)
    {
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.AddColumns("Id", "Start Time", "End Time", "Duration");

        foreach (var session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString("MM-dd-yy H:mm"),
                session.EndTime?.ToString("MM-dd-yy H:mm") ?? "Invalid Time.",
                session.Duration.ToString(@"hh\:mm")
                );
        }
        Console.Clear();
        AnsiConsole.Write(table);
    }

    public void RenderSessionsReport(int sessionsCount, TimeSpan totalDuration, TimeSpan averageDuration)
    {
        var table = new Table();
        table.Border = TableBorder.Rounded;

        table.AddColumns("Session Count", "Total Coding Time", "Average Cooding Time");
        table.Columns[0].RightAligned();
        table.Columns[1].Centered();
        table.Columns[2].Centered();

        table.AddRow(sessionsCount.ToString(), totalDuration.ToString(@"hh\:mm"), averageDuration.ToString(@"hh\:mm"));

        Console.Clear();
        AnsiConsole.Write(table);
    }
}
