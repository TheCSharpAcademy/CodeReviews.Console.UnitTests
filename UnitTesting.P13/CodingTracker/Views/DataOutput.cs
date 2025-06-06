using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker.Views
{
    internal class DataOutput
    {
        public static void PrintProjectData(string Project = "test_project", string ascDesc = "Asc", string option = "All data")
        {
            Table table = new();
            table.Centered();
            table.Title(Project);

            List<CodingSession> projectData;

            if (option == "All data")
            {
                table.AddColumns("Id", "Start Date", "Start Time", "End Date", "End Time", "Duration");
                projectData = DataTools.GetProjectData(Project, ascDesc);
                TimeSpan totalDuration = new();
                if (projectData != null)
                {
                    foreach (CodingSession data in projectData)
                    {
                        totalDuration.Add(TimeSpan.ParseExact(data.Duration, "c", CultureInfo.InvariantCulture, TimeSpanStyles.None));
                        table.AddRow(data.Rowid.ToString(), data.StartDate, data.StartTime, data.EndDate, data.EndTime, data.Duration);
                    }
                    AnsiConsole.Write(table);
                    table = new();
                    table.Centered();
                    table.AddColumns("First Session", "Last Session", "Total session", "Total Duration");

                    Console.WriteLine();
                }
            }
            else
            {
                DateTime currentDate = DateTime.Today.Date;
                string currentWeekDay = DateTime.Today.Date.DayOfWeek.ToString();

                projectData = DataTools.GetProjectData(Project, ascDesc, option);
                if (projectData != null)
                {
                    switch (option)
                    {
                        case "Weekly":
                            table.AddColumns("Year", "Month (n°)", "Week Start", "Week end", "Number of session", "Total duration");
                            foreach (CodingSession data in projectData)
                            {
                                if (data.Project == "Final Result")
                                {
                                    AnsiConsole.Write(table);
                                    table = new();
                                    table.Centered();
                                    table.AddColumns("First session", "Last session", "Total session n°", "Final duration", "Average Duration");
                                    table.AddRow(data.EndDate.Substring(0, 10), data.StartDate.Substring(0, 10), data.DurationCount, data.TotalDuration, data.Average);
                                    AnsiConsole.Write(table);
                                }
                                else
                                {
                                    table.AddRow(data.StartDate.Substring(0, 4), data.StartDate.Substring(5, 2),
                                    data.StartDate, data.EndDate, data.DurationCount, data.TotalDuration);
                                }
                            }
                            Console.WriteLine();

                            break;

                        case "Monthly":
                            table.AddColumns("Year", "Month (n°)", "Number of session", "Total duration");
                            foreach (CodingSession data in projectData)
                            {
                                if (data.Project == "Final Result")
                                {
                                    AnsiConsole.Write(table);
                                    table = new();
                                    table.Centered();
                                    table.AddColumns("First session", "Last session", "Total session n°", "Final duration", "Average Duration");
                                    table.AddRow(data.EndDate.Substring(0, 10), data.StartDate.Substring(0, 10), data.DurationCount, data.TotalDuration, data.Average);
                                    AnsiConsole.Write(table);
                                }
                                else
                                {
                                    table.AddRow(data.StartDate.Substring(0, 4), data.StartDate.Substring(5, 2), data.DurationCount, data.TotalDuration);
                                }
                            }
                            Console.WriteLine();
                            break;

                        case "Yearly":
                            table.AddColumns("Year", "Number of session", "Total duration");
                            foreach (CodingSession data in projectData)
                            {
                                if (data.Project == "Final Result")
                                {
                                    AnsiConsole.Write(table);
                                    table = new();
                                    table.Centered();
                                    table.AddColumns("First session", "Last session", "Total session n°", "Final duration", "Average Duration");
                                    table.AddRow(data.EndDate.Substring(0, 10), data.StartDate.Substring(0, 10), data.DurationCount, data.TotalDuration, data.Average);
                                    AnsiConsole.Write(table);
                                }
                                else
                                {
                                    table.AddRow(data.StartDate.Substring(0, 4), data.DurationCount, data.TotalDuration);
                                }
                            }
                            Console.WriteLine();
                            break;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine($"The project [red bold]{Project}[/] contains no data");
                }
            }
        }

        public static void ShowGoal(CodingSession session)
        {
            try
            {
                if (session != null)
                {
                    Table table = new Table();
                    table.Centered();
                    table.AddColumns("Project", "Start", "End", "Estimate time", "Estimate time per day");
                    table.AddRow(session.Project, session.StartDate, session.EndDate, session.TotalDuration, session.Duration);
                    AnsiConsole.Write(table);
                }
                else
                {
                    AnsiConsole.WriteLine("Goals seems to not have been correctly set, please delete this Coding Goal project and create a new one.");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}