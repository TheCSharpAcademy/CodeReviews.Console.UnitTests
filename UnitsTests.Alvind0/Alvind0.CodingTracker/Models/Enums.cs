namespace Alvind0.CodingTracker.Models;

public class Enums
{
    public enum SortOrder
    {
        Default, Ascending, Descending
    }

    public enum SortType
    {
        Default, Id, Date, Duration
    }

    public enum PeriodFilter
    {
        ThisWeek, ThisMonth, ThisYear, None
    }

    public enum StopwatchState
    {
        Default, Running
    }

    public enum MenuOption
    {
        // Main menu
        StartSession, ManuallyLog, Goals, CodingRecords, Exit,

        // Goal Menu (5, 4)
        ViewGoals, AddGoal, EditGoal, RemoveGoal,

        // Record Menu (9, 4)
        ViewRecords, EditRecord, DeleteRecord, ShowReport,

        // 13
        Return,
    }
}
