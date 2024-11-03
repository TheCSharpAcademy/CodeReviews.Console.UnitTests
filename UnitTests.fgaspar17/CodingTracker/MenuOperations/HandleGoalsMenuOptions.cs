using CodingTrackerLibrary;
using Spectre.Console;

namespace CodingTracker;

public static class HandleGoalsMenuOptions
{
    public static void HandleGoals(string connectionString)
    {
        bool continueRunning = true;
        while (continueRunning)
        {
            GoalsMenu goalsMenu = new GoalsMenu();
            GoalsMenuOptions option = DisplayMenu.ShowMenu<GoalsMenuOptions>(goalsMenu);

            switch (option)
            {
                case GoalsMenuOptions.Quit:
                    continueRunning = false;
                    break;
                case GoalsMenuOptions.Create:
                    HandleCreateGoal(connectionString);
                    break;
                case GoalsMenuOptions.Update:
                    HandleUpdateGoal(connectionString);
                    break;
                case GoalsMenuOptions.Delete:
                    HandleDeleteGoal(connectionString);
                    break;
                case GoalsMenuOptions.Show:
                    CodingGoalOrganizer.ShowCodingGoals(connectionString);
                    break;
                case GoalsMenuOptions.HoursNeeded:
                    CodingGoalOrganizer.ShowCodingGoalsAnalysis(connectionString);
                    break;
                default:
                    break;
            }
        }
    }

    private static void HandleCreateGoal(string connectionString)
    {
        try
        {
            Console.Clear();
            var rule = new Rule("[blue]Inserting[/]");
            AnsiConsole.Write(rule);
            DateTime goalStartTime = Convert.ToDateTime(DisplayMenu.ShowMenu<string>(new StartTimeMenu()));
            DateTime goalEndTime = Convert.ToDateTime(DisplayMenu.ShowMenu<string>(new GoalsEndTimeMenu(goalStartTime)));
            string name = DisplayMenu.ShowMenu<string>(new GoalsNameMenu());
            int hours = DisplayMenu.ShowMenu<int>(new GoalsHoursMenu());
            CodingGoalController.InsertCodingGoal(new CodingGoal { Name = name, StartTime = goalStartTime, EndTime = goalEndTime, Hours = hours }, connectionString);
            Console.WriteLine("Record inserted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void HandleUpdateGoal(string connectionString)
    {
        try
        {
            Console.Clear();
            var rule = new Rule("[yellow]Updating[/]");
            AnsiConsole.Write(rule);
            CodingGoalOrganizer.ShowCodingGoals(connectionString);
            int goalId = DisplayMenu.ShowMenu<int>(new GoalsIdMenu());
            string name = DisplayMenu.ShowMenu<string>(new GoalsNameMenu());
            DateTime goalStartTime = Convert.ToDateTime(DisplayMenu.ShowMenu<string>(new StartTimeMenu()));
            DateTime goalEndTime = Convert.ToDateTime(DisplayMenu.ShowMenu<string>(new GoalsEndTimeMenu(goalStartTime)));
            int hours = DisplayMenu.ShowMenu<int>(new GoalsHoursMenu());
            CodingGoalController.UpdateCodingGoal(new CodingGoal { Id = goalId, Name = name, StartTime = goalStartTime, EndTime = goalEndTime, Hours = hours,  }, connectionString);
            Console.WriteLine("\nRecord updated successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static void HandleDeleteGoal(string connectionString)
    {
        try
        {
            Console.Clear();
            var rule = new Rule("[red]Deleting[/]");
            AnsiConsole.Write(rule);
            CodingGoalOrganizer.ShowCodingGoals(connectionString);
            int goalId = DisplayMenu.ShowMenu<int>(new GoalsIdMenu());
            CodingGoalController.DeleteCodingGoal(new CodingGoal { Id = goalId }, connectionString);
            Console.WriteLine("\nRecord deleted successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
