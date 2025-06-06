using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Views
{
    internal class MainMenu
    {
        private string Option { get; set; }
        private static MenuActions MenuActions { get; set; }
        internal MainMenu()
        {
            MenuActions = new();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Welcome to [blue bold]Coding Tracker[/]");

                Option = MenuActions.SelectOption();

                switch (Option)
                {
                    case "Start/End new coding session":
                        bool IsCodingSessionRunning = MenuActions.MenuModel.IsCodingSessionRunning;
                        if (IsCodingSessionRunning)
                        {
                            IsCodingSessionRunning = UserInputs.ValidateInput("End current coding session?)", choice1: "n", choice2: "y");

                            if (!IsCodingSessionRunning)
                            {
                                MenuActions.EndCurrentSession();
                            }
                        }
                        else if (!IsCodingSessionRunning)
                        {
                            MenuActions.BeginNewCodingSession();
                        }
                        break;

                    case "Insert new data":
                        MenuActions.InsertNewData();
                        break;

                    case "Delete data":
                        MenuActions.DeleteData();
                        break;

                    case "Update data":
                        MenuActions.UpdateData();
                        break;

                    case "Delete project":
                        MenuActions.DeleteProject();
                        break;

                    case "Print single project report":
                        AnsiConsole.Clear();
                        MenuActions.PrintSingleProjectReport();
                        break;

                    case "Print all data":
                        AnsiConsole.Clear();
                        MenuActions.PrintAllData();
                        break;

                    case "Set/Show Coding Goals":
                        MenuActions.SeeShowGoals();
                        break;

                    case "See current session duration":
                        MenuActions.ShowCurrentSession();
                        break;

                    case "Exit":
                        Console.Clear();
                        AnsiConsole.MarkupLine("Thank you for using [yellow bold]CodingTracker![/]");
                        AnsiConsole.WriteLine("Press any key to exit the program");
                        break;

                    case "Fill database for testing purpose":
                        DataTools.AddDataToDB();
                        Console.Read();
                        break;
                    default:
                        break;
                }
            } while (Option != "Exit");

        }
    }
}