using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Views
{
    internal class MainMenu
    {
        private string Option { get; set; }
        private static MenuActions menuActions { get; set; }
        internal MainMenu()
        {
            menuActions = new();

            do
            {
                Console.Clear();
                AnsiConsole.MarkupLine("Welcome to [blue bold]Coding Tracker[/]");

                Option = menuActions.SelectOption();

                switch (Option)
                {
                    case "Start/End new coding session":
                        bool IsCodingSessionRunning = menuActions.menuModel.IsCodingSessionRunning;
                        if (IsCodingSessionRunning)
                        {
                            IsCodingSessionRunning = UserInputs.ValidateInput("End current coding session?)", choice1: "n", choice2: "y");

                            if (!IsCodingSessionRunning)
                            {
                                menuActions.EndCurrentSession();
                            }
                        }
                        else if (!IsCodingSessionRunning)
                        {
                            menuActions.BeginNewCodingSession();
                        }
                        break;

                    case "Insert new data":
                        menuActions.InsertNewData();
                        break;

                    case "Delete data":
                        menuActions.DeleteData();
                        break;

                    case "Update data":
                        menuActions.UpdateData();
                        break;

                    case "Delete project":
                        menuActions.DeleteProject();
                        break;

                    case "Print single project report":
                        AnsiConsole.Clear();
                        menuActions.PrintSingleProjectReport();
                        break;

                    case "Print all data":
                        AnsiConsole.Clear();
                        menuActions.PrintAllData();
                        break;

                    case "Set/Show Coding Goals":
                        menuActions.SeeShowGoals();
                        break;

                    case "See current session duration":
                        menuActions.ShowCurrentSession();
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