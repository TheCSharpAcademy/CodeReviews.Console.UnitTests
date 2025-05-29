using CodingTracker.KamilKolanowski.Data;
using CodingTracker.KamilKolanowski.Enums;

namespace CodingTracker.KamilKolanowski.Controllers;

internal interface IBaseController
{
    void AddSessionManually(DatabaseManager databaseManager);
    void StartSession(bool isSessionStarted);
    void EndSession(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager);
    void ViewSessions(DatabaseManager databaseManager);
    void GetReports();
    void QuitApplication(bool isSessionStarted, bool isSessionEnded, DatabaseManager databaseManager);
}