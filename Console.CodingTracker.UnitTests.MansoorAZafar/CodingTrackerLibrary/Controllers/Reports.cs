using CodingTrackerLibrary.Controllers.Database;
using CodingTrackerLibrary.Models;
using CodingTrackerLibrary.Views;
using Spectre.Console;

namespace CodingTrackerLibrary.Controllers;
internal class Reports
{
    private DatabaseManager databaseManager;
    public Reports(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    private ReportSelections ViewAndGetReportOptions()
    {
        DataViewer.DisplayHeader("Reports", "left");
        System.Console.WriteLine(" These reports will show total hours for the given dates\n");
        System.Console.WriteLine(" Select Which Report?\n");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<ReportSelections>()
            .Title("[green]Choose Your Selection[/]")
            .PageSize(10)
            .AddChoices(new[] {
                ReportSelections.exit, ReportSelections.startFromXDaysAgo,
                ReportSelections.dateToToday, ReportSelections.totalAllTime,
                ReportSelections.startToEnd, ReportSelections.totalForMonth,
                ReportSelections.filter
            }));
        return selection;
    }

    public void HandleReportSelection()
    {
        ReportSelections selection = ViewAndGetReportOptions();

        switch (selection)
        {
            case ReportSelections.startFromXDaysAgo:
                this.StartFromXDaysAgo();
                break;
            
            case ReportSelections.dateToToday:
                this.DateToToday();
                break;
            
            case ReportSelections.totalAllTime:
                this.TotalQuantityOfAllTime();
                break;
            
            case ReportSelections.startToEnd:
                this.StartToEnd();
                break;
            
            case ReportSelections.totalForMonth:
                this.TotalForAGivenMonth();
                break;
            
            case ReportSelections.filter:
                this.Filter();
                break;
            default:
                break;
        }
    }

    private void StartFromXDaysAgo()
    {
        int daysAgo = 0;
        const int DaysInAYear = 365;
        
        Utilities.GetValidIntegerInput
        (
            input: ref daysAgo,
            message: "Please enter the How many days ago\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Number\n> ",
            lowRange: 0,
            maxRange: (Convert.ToInt32(DateTime.Now.ToString("yyyy")) * DaysInAYear) 
        );
        //The date from X days ago can only be an existing one otherwise it'll crash
        // so we take the current year -> turn into an int
        // multiply it by the days in a year so we can include all the days within the entire time-range

        DateTime startDate = DateTime.Now;
        startDate = startDate.AddDays(-daysAgo); // we want to subtract so we use negative

        DateTime endDate = DateTime.Now;
        System.Console.Clear();

        DataViewer.DisplayHeader($"Displaying Dates from {startDate:yyyy-MM-dd} till now");
        DataViewer.DisplayListAsTable<CodingSession>(CodingSession.headers, this.databaseManager.GetDataFromDate(start: ref startDate, end: ref endDate));

        Utilities.PressToContinue();
    }

    private void DateToToday()
    {
        DateTime startDate = DateTime.Now;
        
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref startDate,
            message: "Please enter the Starting Date and Hour (yyyy-MM-dd-HH)\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Date\n> "
        );

        DateTime endDate = DateTime.Now;

        System.Console.Clear();
        
        DataViewer.DisplayHeader($"Displaying Dates from {startDate:yyyy-MM-dd-HH} till now\n");
        DataViewer.DisplayListAsTable<CodingSession>(CodingSession.headers,this.databaseManager.GetDataFromDate(start: ref startDate, end: ref endDate));
        
        Utilities.PressToContinue();
    }

    private void TotalQuantityOfAllTime()
    {
        System.Console.Clear();
        
        DataViewer.DisplayHeader("Total Quantity");
        DataViewer.DisplayHeader(this.databaseManager.GetTotalQuantity() + " hours", "left");

        Utilities.PressToContinue();
    }

    private void StartToEnd()
    {
        DateTime startDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref startDate,
            message: "Please enter the Starting Date and Hour (yyyy-MM-dd-HH)\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Date (yyyy-MM-dd-HH)\n> "
        );

        DateTime endDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref endDate,
            message: "Please enter the Ending Date and Hour (yyyy-MM-dd-HH)\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Date (yyyy-MM-dd-HH)\n> "
        );

        System.Console.Clear();
        
        DataViewer.DisplayHeader($"Displaying Dates from {startDate:yyyy-MM-dd-HH} till {endDate:yyyy-MM-dd-HH}");
        DataViewer.DisplayListAsTable<CodingSession>(CodingSession.headers, this.databaseManager.GetDataFromDate(start: ref startDate, end: ref endDate));
        
        Utilities.PressToContinue();
    }

    private void TotalForAGivenMonth()
    {
        System.Console.Clear();
        int month = 0;
        const int MonthLowerRange = 1;
        const int MonthUpperRange = 12;
        Utilities.GetValidIntegerInput
        (
            input: ref month,
            message: "Enter the Month Number\n> ",
            errorMessage: "Invalid Month\nPlease Enter the Month's Number\n> ",
            lowRange: MonthLowerRange,
            maxRange: MonthUpperRange
        ); 

        DataViewer.DisplayHeader($"Total Quantity For the {month} Month");
        DataViewer.DisplayHeader($"{this.databaseManager.GetTotalQuantityFromMonth(ref month)} hours", "left");

        Utilities.PressToContinue();
    }

    private void Filter()
    {
        Period period = this.GetPeriodSelection();
        SortingSelections sortingBy = this.GetSortingSelection();
        
        System.Console.Clear();

        DataViewer.DisplayHeader($"Filtered Results by {sortingBy} Ordered By {period}\n");
        DataViewer.DisplayListAsTable<CodingSession>(CodingSession.headers, 
                                                     this.databaseManager.GetDataSortedBy(sortingBy, period));
        Utilities.PressToContinue();
    }

    private Period GetPeriodSelection()
    {
        System.Console.Clear();
        DataViewer.DisplayHeader("Period Selection");
        System.Console.WriteLine(" Select Which Period?\n");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<Period>()
            .Title("[green]Choose Your Selection[/]")
            .PageSize(10)
            .AddChoices(new[] {
                Period.Hours, 
                Period.Days, 
                Period.Weeks
            }));

        return selection;
    }

    private SortingSelections GetSortingSelection()
    {
        System.Console.Clear();
        DataViewer.DisplayHeader("Sorting Selection");
        System.Console.WriteLine(" Select Which Period?\n");

        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<SortingSelections>()
            .Title("[green]Choose Your Selection[/]")
            .PageSize(10)
            .AddChoices(new[] {
                SortingSelections.ASC,
                SortingSelections.DESC
            }));

        return selection;
    }
}
