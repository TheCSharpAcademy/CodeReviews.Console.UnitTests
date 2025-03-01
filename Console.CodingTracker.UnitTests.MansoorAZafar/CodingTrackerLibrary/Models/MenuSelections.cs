namespace CodingTrackerLibrary.Models;

 public enum MenuSelections
 {
     None   = -1,
     exit   = 0,
     update = 1,
     delete,
     insert,
     data,
     reports,
     goals
 }

 public enum ReportSelections
 {
     None = -1, 
     exit = 0,
     startFromXDaysAgo = 1,
     dateToToday = 2,
     totalAllTime = 3,
     startToEnd,
     totalForMonth,
     filter
 }

public enum SortingSelections
{
    ASC, 
    DESC
}
public enum Period
{
    Hours = 1,
    Days = Hours * 24,
    Weeks = Days * 7
}