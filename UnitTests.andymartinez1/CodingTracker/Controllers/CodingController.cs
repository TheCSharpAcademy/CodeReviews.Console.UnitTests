using Coding_Tracker.Services;
using Coding_Tracker.Utils;
using Coding_Tracker.Views;

namespace Coding_Tracker.Controllers;

public class CodingController
{
    private readonly ICodingService _codingService;

    public CodingController(ICodingService codingService)
    {
        _codingService = codingService;
    }

    public void GetAllSessions()
    {
        var sessions = _codingService.GetAllSessions();

        UserInterface.ViewAllSessions(sessions);
    }

    public void GetSession()
    {
        var sessions = _codingService.GetAllSessions();

        var sessionId = Helpers.GetSessionId(sessions);

        var session = _codingService.GetSession(sessionId);

        UserInterface.ViewSessionDetails(session);
    }

    public void AddSession()
    {
        _codingService.AddSession();
    }

    public void UpdateSession()
    {
        var sessions = _codingService.GetAllSessions();

        var sessionId = Helpers.GetSessionId(sessions);

        var session = _codingService.GetSession(sessionId);

        _codingService.UpdateSession(session);
    }

    public void DeleteSession()
    {
        var sessions = _codingService.GetAllSessions();

        var sessionId = Helpers.GetSessionId(sessions);

        _codingService.DeleteSession(sessionId);
    }
}
