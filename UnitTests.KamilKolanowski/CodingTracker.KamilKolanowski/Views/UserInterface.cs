using CodingTracker.KamilKolanowski.Controllers;
using CodingTracker.KamilKolanowski.Enums;
using CodingTracker.KamilKolanowski.Data;
using Spectre.Console;

namespace CodingTracker.KamilKolanowski.Views;

internal class UserInterface
{
    private readonly DatabaseManager _databaseManager = new();
    private readonly SessionsController _sessionsController = new();
    
    private bool _isSessionStarted;
    private bool _isSessionEnded;
    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            
            var operationChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .AddChoices(Options.OptionDisplayNames.Values));

            var selectedOption = Options.OptionDisplayNames
                .FirstOrDefault(x => x.Value == operationChoice).Key;
            
            
            switch (selectedOption)
            {
                case Options.MenuOptions.AddSessionManually:
                    AddSessionManually(_databaseManager);
                    break;
                case Options.MenuOptions.StartSession:
                    StartSession(_isSessionStarted);
                    _isSessionStarted = true;
                    _isSessionEnded = false;
                    break;
                case Options.MenuOptions.EndSession:
                    _isSessionEnded = true;
                    EndSession(_isSessionStarted, _isSessionEnded, _databaseManager);
                    _isSessionStarted = false;
                    break;
                case Options.MenuOptions.ViewSessions:
                    ViewSessions();
                    break;
                case Options.MenuOptions.GetReport:
                    GetReports();
                    break;
                case Options.MenuOptions.Quit:
                    QuitApplication(_isSessionStarted, _isSessionEnded, _databaseManager);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void AddSessionManually(DatabaseManager databaseManager)
    {
        _sessionsController.AddSessionManually(databaseManager);
    }
    private void StartSession(bool isSessionStarted)
    {
        _sessionsController.StartSession(isSessionStarted);
    }

    private void EndSession(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager)
    {
        _sessionsController.EndSession(isSessionStarted, isSessionEnded, databaseManager);
    }
    
    public void ViewSessions()
    {
        _sessionsController.ViewSessions(_databaseManager);
    }

    private void GetReports()
    {
        _sessionsController.GetReports();
    }

    public void QuitApplication(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager)
    {
        _sessionsController.QuitApplication(isSessionStarted, isSessionEnded, databaseManager);
    }
}