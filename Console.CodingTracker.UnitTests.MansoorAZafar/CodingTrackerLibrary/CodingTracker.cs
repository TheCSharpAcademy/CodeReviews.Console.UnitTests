using CodingTrackerLibrary.Models;
using CodingTrackerLibrary.Controllers;
using Spectre.Console;
using CodingTrackerLibrary.Views;

namespace CodingTrackerLibrary;
public class CodingTracker
{
    private CodingHabitManager manager = new();
    public void Start()
    {
        MenuSelections selection = MenuSelections.None;
        while (selection != MenuSelections.exit)
        {
            DataViewer.DisplayHeader("Coding Habit Selection");
            DataViewer.DisplayHeader("Main Menu", "left");
            
            selection = this.Menu();
            manager.DoMenuAction(selection);
        }

        //Exiting Message(s)
        DataViewer.DisplayHeader("La Fin");
        AnsiConsole.Write(
            new FigletText("Goodbye!")
                .LeftJustified()
                .Color(Color.Red));
    }

    private MenuSelections Menu()
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<MenuSelections>()
            .Title("[green]Choose Your Selection[/]")
            .PageSize(10)
            .AddChoices(new[] {
                MenuSelections.exit, MenuSelections.update, MenuSelections.delete,
                MenuSelections.insert, MenuSelections.data, MenuSelections.reports,
                MenuSelections.goals
            }));
        return selection;
    }
}
