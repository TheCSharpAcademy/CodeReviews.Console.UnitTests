namespace CodingTracker
{
    internal enum MenuOption
    {
        ViewSessions,
        CurrentCodingSession,
        ViewGoals,
        AddGoal,
        DeleteRecord,
        DeleteGoal,
        EditSession,
        GenerateReport,
        Quit
    }

    internal enum CurrentCodingSessionChoice
    {
        StartSession,
        EndSession,
        EditSession,
        GoBack
    }

    internal enum EditSessionChoice
    {
        StartTime,
        EndTime,
        GoBack
    }

    internal enum RecordsFilterPeriodMenu { 
        All,
        ByPeriod
    }

    internal enum RecordsFilterFieldMenu
    {
        Duration,
        Date
    }

    internal enum RecordsFilterOrderMenu { 
        Ascending,
        Descending
    }

}
