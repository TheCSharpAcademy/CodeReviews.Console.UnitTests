using CodingTrackerLibrary;
using System.Globalization;

namespace CodingTracker;

public static class CodingSessionOrganizer
{
    public static void ShowCodingSessions(string connectionString, RecordsMenuOptions option = RecordsMenuOptions.All, OrderOptions order = OrderOptions.Asc)
    {
        List<CodingSession> sessions = CodingSessionController.GetCodingSessions(connectionString);

        sessions = FilterSessions(sessions, option);
        sessions = SortSessions(sessions, order);

        OutputRenderer.ShowTable<CodingSession>(sessions, title: "Coding Sessions");
    }

    private static List<CodingSession> FilterSessions(List<CodingSession> sessions, RecordsMenuOptions option)
    {
        DateTime today = DateTime.Today;
        switch (option)
        {
            case RecordsMenuOptions.Day:
                return sessions.Where(s => s.StartTime.Date == today).ToList();
            case RecordsMenuOptions.Week:
                int currentWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                    today, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                return sessions.Where(s =>
                    CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                        s.StartTime, CalendarWeekRule.FirstDay, DayOfWeek.Monday) == currentWeek
                ).ToList();
            case RecordsMenuOptions.Year:
                return sessions.Where(s => s.StartTime.Year == today.Year).ToList();
            default:
                return sessions;
        }
    }

    private static List<CodingSession> SortSessions(List<CodingSession> sessions, OrderOptions order)
    {
        return order == OrderOptions.Asc ?
            sessions.OrderBy(s => s.StartTime).ToList() :
            sessions.OrderByDescending(s => s.StartTime).ToList();
    }
}
