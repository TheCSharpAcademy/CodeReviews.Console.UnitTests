using CodingTracker.Controllers;
using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Views;

public class AppMenu
{
    private readonly DbController _dbController;

    public AppMenu()
    {
        _dbController = new DbController();
    }

    public void Show()
    {
        while (true)
        {
            var choice = UserInput.GetUserChoice();
            var sessions = new List<CodingSession>();
            AnsiConsole.Clear();
            switch (choice)
            {
                case MenuChoices.DisplayRecords:
                    sessions = _dbController.GetAllRecords();
                    TableView.ShowTable(sessions);
                    break;
                case MenuChoices.InsertRecord:
                    var recordInput = UserInput.GetUserRecordInput();
                    _dbController.Insert(recordInput);
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[green] Added succesfully! [/]");
                    break;
                case MenuChoices.RemoveRecord:
                    sessions = _dbController.GetAllRecords();
                    var idToRemove = UserInput.GetUserId(sessions);
                    if (idToRemove == null)
                    {
                        AnsiConsole.MarkupLine("[white] No coding records to remove, create one first![/]");
                        continue;
                    }

                    _dbController.Remove(idToRemove);
                    AnsiConsole.MarkupLine("[green] Removed succesfully! [/]");
                    break;
                case MenuChoices.EditRecord:
                    sessions = _dbController.GetAllRecords();
                    var idToUpdate = UserInput.GetUserId(sessions);
                    if (idToUpdate == null)
                    {
                        AnsiConsole.MarkupLine("[white] No coding records, create one first![/]");
                        continue;
                    }

                    var updatedRecord = UserInput.GetUserRecordInput();
                    _dbController.Update(idToUpdate, updatedRecord);
                    AnsiConsole.Clear();
                    AnsiConsole.MarkupLine("[green] Edited succesfully! [/]");
                    break;
                case MenuChoices.Exit:
                    AnsiConsole.MarkupLine("[Green] Goodbye! [/]");
                    return;
            }
        }
    }
}