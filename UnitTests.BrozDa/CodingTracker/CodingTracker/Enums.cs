/// <summary>
/// Represents user choice in the menu
/// </summary>
internal enum UserChoice
{
    View = 1,
    Insert,
    Delete,
    Update,
    Track,
    Report,
    Exit
}

/// <summary>
/// Represents time frame requested by the user
/// </summary>
internal enum ReportTimeFrame
{
    ThisYear,
    ThisMonth,
    ThisWeek,
    Custom,
}