using CodingTracker.Controllers;
using CodingTracker.Data;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker
{
    internal class TableVisualizationEngine
    {
        static public Table PrepareTable(string type)
        {
            Table table = new();
            if (type == "sessions")
            {
                table.AddColumn(new TableColumn("Id").Centered());
                table.AddColumn(new TableColumn("Start Time").Centered());
                table.AddColumn(new TableColumn("End Time").Centered());
                table.AddColumn(new TableColumn("Duration").Centered());
                return table;
            }
            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn(new TableColumn("Days Left").Centered());
            table.AddColumn(new TableColumn("Desired Session Goal").Centered());
            table.AddColumn(new TableColumn("Left to Achieve").Centered());
            table.AddColumn(new TableColumn("Hours/Day to Achieve Goal").Centered());
            table.AddColumn(new TableColumn("Active").Centered());
            return table;
        }

        static public void VisualizeSessions(List<CodingSession> sessions)
        {
            if (sessions == null) return;
            Table table = PrepareTable("sessions");
            foreach (CodingSession session in sessions) {
                if (session.EndTime != null)
                {
                    table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, TimeController.ConvertFromSeconds(session.Duration));
                }
                else
                {
                    table.AddRow(session.Id.ToString(), session.StartTime);
                }
            }
            AnsiConsole.Write(table);
            AnsiConsole.Console.Input.ReadKey(false);
        }

        static public void VisualizeSession(string id) {
            CodingSession? session = DbConnection.GetSession(id);
            if (session == null) return;
            Table table = PrepareTable("sessions");
            table.AddRow(session.Id.ToString(), session.StartTime, session.EndTime, session.Duration.ToString());
            AnsiConsole.Write(table);
        }

        static public void VisualizeGoals(List<Goal> goals)
        {
                if (goals == null) return;
                Table table = PrepareTable("goals");
                foreach (Goal goal in goals)
                {
                    double daysLeft = Math.Round((goal.PeriodInDays - (double)(DateTime.Now - DateTime.Parse(goal.StartDate)).TotalDays), 1);
                    int totalDurationOfSessionUntilNow = DbConnection.GetSessionsDurationsByPeriod(DateTime.Parse(goal.StartDate).ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    int leftToAchieve = (goal.DesiredLengthInSeconds - totalDurationOfSessionUntilNow) / 60 / 60;
                    if (leftToAchieve <= 0 && goal.IsActive)
                    {
                        goal.IsActive = false;
                        DbConnection.UpdateGoal(goal);
                    }
                    table.AddRow(goal.Id.ToString(), $"{daysLeft} days", $"{goal.DesiredLengthInSeconds / 60 / 60} hours", $"{leftToAchieve} hours", $"{Math.Round((leftToAchieve / daysLeft), 2)} hours", goal.IsActive.ToString());
                }
                AnsiConsole.Write(table);
                AnsiConsole.Console.Input.ReadKey(false);
        }
    }
}
