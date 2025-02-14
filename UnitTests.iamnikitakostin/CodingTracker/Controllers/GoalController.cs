using CodingTracker.Data;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Controllers
{
    public class GoalController : ConsoleController
    {
        static public bool Add()
        {
            try
            {
                Goal goal = new Goal();
                int numberOfDays = AnsiConsole.Prompt(
                new TextPrompt<int>("Please, enter the number of days you would like to set your goals for (e.g. entering '30', will send the goal end date to 30 days from now."));
                int hours = AnsiConsole.Prompt(
                new TextPrompt<int>("Please, enter the desired amount of hours you would like to code for in the mentioned period:"));
                string dateInput = AnsiConsole.Prompt(
                    new TextPrompt<string>("Please, enter the start date of the goal ([yellow]YYYY-MM-DD[/]):")
                        .Validate(date =>
                        {
                            if (DateOnly.TryParse(date, out _))
                                return ValidationResult.Success();
                            return ValidationResult.Error("[red]Invalid date format. Please use YYYY-MM-DD.[/]");
                        }));
                DateOnly? date = DateChecker(dateInput);

                goal.StartDate = date?.ToString("yyyy-MM-dd") ?? throw new Exception("The date is wrong");
                goal.DesiredLengthInSeconds = hours * 60 * 60;
                goal.PeriodInDays = numberOfDays;
                goal.IsActive = true;
                DbConnection.AddGoal(goal);

                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message);
                return false;
            }
        }

        public static DateOnly? DateChecker(string dateInput)
        {
            DateOnly date = new();
            DateOnly.TryParseExact(dateInput, "yyyy-MM-dd", out date);

            if (date.Year == 1)
            {
                return null;
            }
            return date;
        }

        internal static bool Delete()
        {
            GetAll();
            string choice = AnsiConsole.Prompt(
            new TextPrompt<string>("Please, enter the id of the goal you would like to delete."));
            if (Int32.TryParse(choice, out _))
            {
                if (DbConnection.DeleteGoal(choice))
                {
                    SuccessMessage("The session has been removed.");
                    return true;
                }
            }
            else
            {
                ErrorMessage("There has been a problem while trying to remove the session. The session might not exist, or the id was mistyped.");
            }
            return false;
        }

            public static void GetAll()
              {
            Console.Clear();
            List<Goal>? goals = DbConnection.GetGoals();
            TableVisualizationEngine.VisualizeGoals(goals);
        }

    }
}
