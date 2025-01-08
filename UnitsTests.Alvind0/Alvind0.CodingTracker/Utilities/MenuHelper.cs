using static Alvind0.CodingTracker.Models.Enums;
namespace Alvind0.CodingTracker.Utilities;

public class MenuHelper
{
    public static readonly Dictionary<MenuOption, string> MenuOptionDescriptions = new()
    {
        { MenuOption.StartSession, "Start Coding Session" },
        { MenuOption.ManuallyLog, "Add Session Manually" },
        { MenuOption.Goals, "Goals" },
        { MenuOption.CodingRecords, "Coding Records" },
        { MenuOption.Exit, "Exit Application" },

        { MenuOption.ViewGoals, "View Goals" },
        { MenuOption.AddGoal, "Add Goal" },
        { MenuOption.EditGoal, "Edit Goal" },
        { MenuOption.RemoveGoal, "Remove Goal" },

        { MenuOption.ViewRecords, "View Records" },
        { MenuOption.EditRecord, "Edit Record" },
        { MenuOption.DeleteRecord, "Delete Record" },
        { MenuOption.ShowReport, "Show Statistics" },

        { MenuOption.Return, "Go Back" },
    };

    public static string[] GetStopwatchMenu(StopwatchState state)
    {
        string[] options;
        switch (state)
        {
            case StopwatchState.Default:
                options = new[] { "Start", "End" };
                break;
            case StopwatchState.Running:
                options = new[] { "End" };
                break;
            //case StopwatchState.Paused:
            //    options = new[] { "Resume", "End" };
            //    break;
            default:
                throw new NullReferenceException($"Option does not exists {state}");
        }

        return options;
    }

    public static string GetMenuOption(MenuOption option)
    {
        var result = "";
        return MenuOptionDescriptions.TryGetValue(option, out result) ? result : throw new NullReferenceException("Menu option does not exist.");
    }

    public static List<string> GetMenuOptions(int startIndex, int count)
        => MenuOptionDescriptions.Values.ToList().Skip(startIndex).Take(count).ToList();

    public static MenuOption GetMenuOptionFromDescription(string description)
    {
        foreach (var option in MenuOptionDescriptions)
        {
            if (option.Value == description)
            {
                return option.Key;
            }
        }
        throw new NullReferenceException($"Invalid menu description: {description}");
    }
}

