using Microsoft.Extensions.Configuration;

namespace CodingTracker;

public class CodingController(IConfiguration config, DapperHelper dapper, SpectreConsole console)
{
    private readonly IConfiguration _config = config;
    private readonly DapperHelper _dapper = dapper;
    private readonly SpectreConsole _console = console;

    public void Run()
    {
        _console.Welcome();

        // main loop
        while (true)
        {
            switch (_console.MainMenu())
            {
                case "Add Session":
                    AddSession();
                    break;
                case "Start New Timed Session":
                    StartNewSession();
                    break;
                case "Review Sessions":
                    ReviewSessions();
                    break;
                case "Edit Session":
                    EditSession();
                    break;
                case "Delete Session":
                    DeleteSession();
                    break;
                case "Delete All Sessions":
                    DeleteAllSessions();
                    break;
                case "Exit":
                    Exit();
                    return;
            }
        }
    }

    public void AddSession()
    {
        (string startTime, string endTime) = _console.AddSession();

        CodingSession session = new(startTime, endTime);
        
        int rowsAffected = _dapper.Insert(session);

        if (rowsAffected == 1)
        {
            _console.Success("Session added.");
        }
        else
        {
            _console.Error("Could not add session.");
        }
    }

    public void ReviewSessions()
    {
        while (true)
        {
            switch (_console.ReviewSessionsMenu())
            {
                case "Get Sessions By...":
                    GetSessionBy();
                    break;
                case "Browse All Sessions":
                    DisplayAllSessions();
                    break;
                case "Exit":
                    return;
            }
        }
    }

    public void DisplayAllSessions()
    {
        var sessions = _dapper.GetAllSessions();
        _console.DisplaySessions(sessions);
    }

    public void GetSessionBy()
    {
        while (true)
        {
            switch (_console.GetSessionByMenu())
            {
                case "Year":
                    string year = _console.GetSessionsByYearMenu();
                    _console.DisplaySessions(_dapper.GetSessionsByYear(year));
                    break;
                case "Month":
                    string month = _console.GetSessionsByMonthMenu();
                    _console.DisplaySessions(_dapper.GetSessionsByMonth(month));
                    break;
                case "Duration":
                    (int min, int max) = _console.GetSessionsByDurationMenu();
                    _console.DisplaySessions(_dapper.GetSessionsByDuration(min, max));
                    break;
                case "Exit":
                    return;
                    
            }
        }
    }

    public void EditSession()
    {
        while (true)
        {
            int id = _console.EditSessionSelectionMenu();

            if (id == 0)
            {
                return;
            }

            if (!_dapper.TryGetSession(id, out CodingSession session))
            {
                _console.Error("Session not found.");
                continue;
            }

            (session.StartTime, session.EndTime) = _console.EditSessionMenu(session);
            
            if (!_dapper.UpdateSession(session))
            {
                _console.Error("Could not update session.");
                continue;
            }

            _console.Success("Session updated.");
            return;
        } 
    }

    public void DeleteSession()
    {
        while (true)
        {
            int id = _console.DeleteSessionSelectionMenu();

            if (id == 0)
            {
                return;
            }

            if (!_dapper.TryGetSession(id, out CodingSession session))
            {
                _console.Error("Session not found.");
                continue;
            }

            if (!_console.DeleteSessionConfirmation(session))
            {
                return;
            }

            if (!_dapper.DeleteSessionById(id))
            {
                _console.Error("Failed to delete session.");
                continue;
            }

            _console.Success("Session deleted.");
            return;
        }
    }

    public void DeleteAllSessions()
    {
        if (!_console.DeleteAllSessionsConfirmation())
        {
            return;
        }
        if (_dapper.DeleteAllSessions())
        {
            _console.Success("All sessions deleted.");
        }
        else
        {
            _console.Error("Failed to delete sessions.");
        }
    }

    public void StartNewSession()
    {
        if (!_console.StartNewSessionPrompt())
            return;

        var startTime = DateTime.Now;
        _console.StartNewSession();
        var endTime = DateTime.Now;

        CodingSession session = new(startTime, endTime);

        if (_dapper.Insert(session) > 0)
        {
            _console.Success("Session added!");
        }
        else
        {
            _console.Error("Could not add session.");
        }
    }

    public void Exit()
    {
        _console.Goodbye();

        if (_config["TeardownData"] is not null and "True")
        {
            _dapper.TeardownDB();
        }
    }
}