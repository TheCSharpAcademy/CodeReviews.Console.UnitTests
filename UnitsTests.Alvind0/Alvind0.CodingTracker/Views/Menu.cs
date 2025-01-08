using Alvind0.CodingTracker.Controllers;
using Alvind0.CodingTracker.Utilities;
using Spectre.Console;
using static Alvind0.CodingTracker.Models.Enums;
namespace Alvind0.CodingTracker.Views;

public class Menu
{
    private readonly CodingSessionController _codingSessionController;
    private readonly GoalController _goalController;
    public Menu(CodingSessionController codingSessionController, GoalController goalController)
    {
        _codingSessionController = codingSessionController;
        _goalController = goalController;
    }
    public async Task MainMenu()
    {

        var isExitApp = false;
        while (true)
        {
            Console.Clear();
            var menuOptions = MenuHelper.GetMenuOptions(0, 5);
            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose a menu: ")
                .AddChoices<string>(menuOptions));

            var selectedOption = MenuHelper.GetMenuOptionFromDescription(userChoice);
            switch (selectedOption)
            {
                case MenuOption.StartSession:
                    await _codingSessionController.RunStopwatch();
                    break;
                case MenuOption.ManuallyLog:
                    _codingSessionController.LogSessionManually();
                    break;
                case MenuOption.Goals:
                    SubMenu(5, 4);
                    break;
                case MenuOption.CodingRecords:
                    SubMenu(9, 4);
                    break;
                case MenuOption.Exit:
                    Console.WriteLine("Goodbye. ");
                    isExitApp = true;
                    break;
                default:
                    break;
            }

            if (isExitApp) break;
        }
    }

    private void SubMenu(int startIndex, int count)
    {
        var isReturnToMainMenu = false;
        while (true)
        {
            var menuOptions = MenuHelper.GetMenuOptions(startIndex, count);
            var returnMenu = MenuHelper.GetMenuOption(MenuOption.Return);
            menuOptions.Add(returnMenu);

            var userChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose a menu: ")
                .AddChoices<string>(menuOptions));

            var selectedOption = MenuHelper.GetMenuOptionFromDescription(userChoice);
            switch (selectedOption)
            {
                case MenuOption.ViewGoals:
                    _goalController.ViewGoals();
                    break;
                case MenuOption.AddGoal:
                    _goalController.AddGoal();
                    break;
                case MenuOption.EditGoal:
                    _goalController.EditGoal();
                    break;
                case MenuOption.RemoveGoal:
                    _goalController.RemoveGoal();
                    break;
                case MenuOption.ViewRecords:
                    _codingSessionController.ShowCodingSessions(true);
                    break;
                case MenuOption.EditRecord:
                    _codingSessionController.EditSession();
                    break;
                case MenuOption.DeleteRecord:
                    _codingSessionController.DeleteSession();
                    break;
                case MenuOption.ShowReport:
                    _codingSessionController.ShowReport();
                    break;
                case MenuOption.Return:
                    isReturnToMainMenu = true;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            if (isReturnToMainMenu) break;
        }
    }
}