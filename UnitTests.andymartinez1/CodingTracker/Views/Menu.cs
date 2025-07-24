using Coding_Tracker.Controllers;
using Coding_Tracker.Enums;
using Coding_Tracker.Utils;
using Spectre.Console;

namespace Coding_Tracker.Views;

public class Menu
{
    private readonly CodingController _codingController;

    private readonly MenuOptions[] _menuOptions =
    [
        MenuOptions.ViewAllSessions,
        MenuOptions.ViewSession,
        MenuOptions.AddSession,
        MenuOptions.UpdateSession,
        MenuOptions.DeleteSession,
        MenuOptions.Quit,
    ];

    public Menu(CodingController codingController)
    {
        _codingController = codingController;
    }

    public void MainMenu()
    {
        var isMenuRunning = true;

        while (isMenuRunning)
        {
            AnsiConsole.Write(new FigletText("Coding Tracker").Color(Color.Aquamarine1));

            var usersChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuOptions>()
                    .Title("Welcome! Please select from the following options:")
                    .AddChoices(_menuOptions)
                    .UseConverter(c => c.GetDisplayName())
            );

            switch (usersChoice)
            {
                case MenuOptions.AddSession:
                    AnsiConsole.Clear();
                    _codingController.AddSession();
                    break;
                case MenuOptions.ViewAllSessions:
                    AnsiConsole.Clear();
                    _codingController.GetAllSessions();
                    break;
                case MenuOptions.ViewSession:
                    AnsiConsole.Clear();
                    _codingController.GetSession();
                    break;
                case MenuOptions.UpdateSession:
                    AnsiConsole.Clear();
                    _codingController.UpdateSession();
                    break;
                case MenuOptions.DeleteSession:
                    AnsiConsole.Clear();
                    _codingController.DeleteSession();
                    break;
                case MenuOptions.Quit:
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine(
                        "[blue]Thank you for using this coding tracker! Press any key to exit. Goodbye![/]"
                    );
                    Console.ReadKey();
                    isMenuRunning = false;
                    Environment.Exit(0);
                    break;
                default:
                    AnsiConsole.Clear();
                    Console.WriteLine("Invalid choice. Please choose one of the above");
                    break;
            }
        }
    }
}
