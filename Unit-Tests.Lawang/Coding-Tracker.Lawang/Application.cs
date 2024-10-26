using Spectre.Console;

namespace Lawang.Coding_Tracker;

public class Application 
{
    private UserInput _UserInput;
    private CodingController _codingController;
    private Visual _visual;
    public Application()
    {
        _UserInput = new UserInput();
        _codingController = new CodingController();
        _visual = new Visual();
    }
    public void MainMenu()
    {
        bool runApp = true;
        while (runApp)
        {
            Console.Clear();

            //To show the project title "Coding Tracker" in figlet text
            _visual.TitlePanel("Coding Tracker");

            // Selecting the Operation that user want to do in application
            var selectedOption = _UserInput.GetMenuOption();

            switch (selectedOption.SelectedValue)
            {
                case 1:
                    ViewAllRecords();

                    AnsiConsole.Markup("[grey](press 'ENTER' to go back to Menu.)[/]");
                    Console.ReadLine();
                    break;
                case 2:
                    Console.Clear();
                    try
                    {
                        var codingRecord = GetApplication();
                        int rowsAffected = _codingController.Post(codingRecord);

                        //renders result (rows affected by inserting) using Spectre.Console
                        _visual.RenderResult(rowsAffected);
                    }
                    catch (ExitOutOfOperationException) { }

                    break;
                case 3:
                    Console.Clear();
                    AnsiConsole.Write(new Rule("[bold aqua]UPDATE RECORD[/]"));
                    try
                    {
                        var codingSessions = _codingController.GetAllData();
                        _visual.RenderTable(codingSessions);
                        AnsiConsole.MarkupLine("[grey](Press '0' to go back to main menu.)[/]");
                        int affectedRow = UpdateRecord();

                        //renders result (rows affected by updating) using Spectre.Console
                        _visual.RenderResult(affectedRow);

                    }
                    catch (ExitOutOfOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case 4:
                    Console.Clear();
                    AnsiConsole.Write(new Rule("[bold aqua]DELETE RECORD[/]"));
                    try
                    {
                        var codingSessions = _codingController.GetAllData();
                        _visual.RenderTable(codingSessions);
                        AnsiConsole.MarkupLine("[grey](Press '0' to go back to main menu.)[/]");
                        int affectedRow = DeleteRecord();

                        //renders result (rows affected by updating) using Spectre.Console
                        _visual.RenderResult(affectedRow);

                    }
                    catch (ExitOutOfOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;

                case 6:
                    UseTimerToRecord();
                    break;

                case 7:
                    Console.Clear();
                    FilterDataAndShowReport();
                    break;


                case 8:
                    Console.Clear();
                    ManageGoals();

                    break;

                case 0:
                    Console.Clear();
                    _visual.TitlePanel("Have a nice day!");
                    runApp = false;
                    break;
            }
        }
    }

    private CodingSession GetApplication()
    {
        var rule = new Rule("[steelblue1_1]Start Time[/]").LeftJustified();
        DateTime startTime = _UserInput.GetUserTime(rule);

        rule = new Rule("[steelblue1_1]End Time[/]").LeftJustified();
        DateTime endTime = _UserInput.GetUserTime(rule);

        /*
            Usually endTime  is greater than startTime;
            so, error should occur if end time is smaller than startTime
            but, 
            Exception -> User start coding at night 11:00 pm till 2:00 am,
            Solution -> Add 1 day to the endTime if it is smaller than startTime.  
        */

        if (endTime < startTime)
        {
            endTime = endTime.AddDays(1);
        }
        TimeSpan duration = endTime - startTime;

        // On which day CodingSession took place
        DateTime sessionDate = _UserInput.ValidateSessionDate();

        var codingSession = new CodingSession()
        {
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration,
            //Date on which startTime was intiated
            Date = sessionDate
        };


        return codingSession;
    }

    private void ViewAllRecords()
    {
        Console.Clear();
        List<CodingSession> codingSessions = _codingController.GetAllData();

        // title of the codingSessions table
        var tableTitle = new Panel(new Markup("[bold underline] CODING - SESSIONS [/]").Centered())
            .Padding(1, 1, 1, 1)
            .Border(BoxBorder.Double)
            .BorderColor(Color.Aqua)
            .Expand();
        AnsiConsole.Write(tableTitle);

        if (codingSessions != null)
            // this renders the table in console
            _visual.RenderTable(codingSessions);
    }

    private int UpdateRecord()
    {
        var codingSessions = _codingController.GetAllData();
        //gets the CodingSession object that needs to be updated
        CodingSession codingSessionToUpdate = _UserInput.GetCodingSession(codingSessions, "update");
        //Update Start Time
        codingSessionToUpdate.StartTime = _UserInput.GetUserTime(new Rule("[steelblue1_1]Update Start-Time[/]").LeftJustified());
        //Update End Time
        codingSessionToUpdate.EndTime = _UserInput.GetUserTime(new Rule("[steelblue1_1]Update End-Time[/]").LeftJustified());

        if (codingSessionToUpdate.StartTime > codingSessionToUpdate.EndTime)
        {
            codingSessionToUpdate.EndTime = codingSessionToUpdate.EndTime.AddDays(1);
        }

        codingSessionToUpdate.Duration = codingSessionToUpdate.EndTime - codingSessionToUpdate.StartTime;

        return _codingController.Update(codingSessionToUpdate);

    }

    private int DeleteRecord()
    {
        var codingSessions = _codingController.GetAllData();
        CodingSession codingSessionToDelete = _UserInput.GetCodingSession(codingSessions, "delete");

        return _codingController.Delete(codingSessionToDelete);
    }

    private void UseTimerToRecord()
    {
        Console.Clear();
        DateTime startTime = DateTime.Now;
        TimeSpan duration = StopTimer.UseTimer();
        Console.Clear();

        //Report on coding Time
        AnsiConsole.MarkupLine($"[green bold]Starting Time[/]: [underline bold]{startTime.ToString("hh:mm:ss tt")}[/]\n");
        DateTime endTime = startTime.Add(duration);
        AnsiConsole.MarkupLine($"[red bold]Ending Time[/]: [underline bold]{endTime.ToString("hh:mm:ss tt")}[/]\n");

        AnsiConsole.Write(new Rule("[yellow]DURATION[/]"));
        AnsiConsole.MarkupLine($"[lightskyblue1 bold]TOTAL CODING TIME[/]: [greenyellow bold]{duration.ToString("hh\\:mm\\:ss")}[/]\n");
        bool userSelection = AnsiConsole.Confirm("Would you like to store this coding session in the database?");


        // Coding session is added to database if user selected "y"
        Console.WriteLine();
        if (userSelection)
        {
            var codingSession = new CodingSession()
            {
                StartTime = startTime,
                EndTime = endTime,
                Duration = duration,
                Date = startTime
            };
            int affectedRow = _codingController.Post(codingSession);

            _visual.RenderResult(affectedRow);
        }
    }

    private void FilterDataAndShowReport()
    {
        List<CodingSession> codingSessions = _codingController.GetAllData();

        Option selectedFilter;

        do
        {
            Console.Clear();
            // Select the type of filter to apply on all the coding Sessions
            selectedFilter = _UserInput.GetFilterOption();

            switch (selectedFilter.SelectedValue)
            {
                case 1:
                    DateTime fromToday = DateTime.Now;
                    List<CodingSession> todaySessions = codingSessions.Where(c => c.Date.Date == fromToday.Date).ToList();
                    DisplayFilterResult(todaySessions);
                    break;

                case 2:
                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    List<CodingSession> yesterdaySessions = codingSessions.Where(c => c.Date.Date == yesterday.Date).ToList();
                    DisplayFilterResult(yesterdaySessions);
                    break;

                case 3:
                    DateTime thisWeek = DateTime.Now.AddDays(-7);
                    List<CodingSession> thisWeekSessions = codingSessions.Where(c => c.Date.Date >= thisWeek.Date).ToList();

                    DisplayFilterResult(thisWeekSessions);
                    break;

                case 4:
                    DateTime lastWeek = DateTime.Now.AddDays(-14);
                    List<CodingSession> lastWeekSessions = codingSessions.Where(c => c.Date.Date >= lastWeek.Date).ToList();
                    DisplayFilterResult(lastWeekSessions);
                    break;

                case 5:
                    DateTime thisMonth = DateTime.Now;
                    List<CodingSession> thisMonthSessions = codingSessions.Where(c =>
                        c.Date.Month == thisMonth.Month && c.Date.Year == thisMonth.Year).ToList();

                    //Display filtered data in a table
                    DisplayFilterResult(thisMonthSessions);
                    break;

                case 6:
                    DateTime thisYear = DateTime.Now;
                    List<CodingSession> thisYearSessions = codingSessions.Where(c => c.Date.Date.Year == thisYear.Year).ToList();

                    //Display filtered data in a table
                    DisplayFilterResult(thisYearSessions);
                    break;

                case 7:
                    DateTime lastYear = DateTime.Now.AddYears(-1);
                    List<CodingSession> lastYearSessions = codingSessions.Where(c => c.Date.Date.Year == lastYear.Year).ToList();

                    //Display filtered data in a table
                    DisplayFilterResult(lastYearSessions);
                    break;

                case 8:
                    List<CodingSession> ascCodingSessions = codingSessions.OrderBy(c => c.Date).ToList();
                    DisplayFilterResult(ascCodingSessions);
                    break;

                case 9:
                    List<CodingSession> descCodingSessions = codingSessions.OrderByDescending(c => c.Date).ToList();
                    DisplayFilterResult(descCodingSessions);

                    break;
                case 0:
                    break;
            }
        } while (selectedFilter.SelectedValue != 0);
    }

    private void DisplayFilterResult(List<CodingSession> codingSessions)
    {
        if (codingSessions.Count > 0)
        {
            _visual.RenderTable(codingSessions);
            Console.WriteLine();
            // Creates a report based on the filtered results
            CreateReport(codingSessions);
        }
        else
        {
            AnsiConsole.Write(new Markup("[red bold][[ NO RECORD FROM TODAY!! ]][/]").Centered());
        }

        AnsiConsole.Write(new Markup("[bold grey](press 'Enter' to continue.)[/]").Centered());
        Console.ReadLine();
    }

    private void CreateReport(List<CodingSession> codingSessions)
    {
        TimeSpan totalTime = TimeSpan.Zero;
        foreach (var codingSession in codingSessions)
        {
            totalTime += codingSession.Duration;
        }
        // first totalTime is converted into Ticks and divided by the total coding session
        TimeSpan averageTimeSpan = new TimeSpan(totalTime.Ticks / codingSessions.Count);
        //draw rule
        Rule rule = new Rule("[seagreen1 on grey0 bold]REPORT[/]").LeftJustified();
        AnsiConsole.Write(rule);
        Console.WriteLine();


        _visual.ShowReport("Total Time", totalTime.ToString(), "yellow");
        _visual.ShowReport("Average Time", averageTimeSpan.ToString(), "green");
    }
    public void ManageGoals()
    {
        while(true)
        {
            Option selection = _UserInput.GetGoalOption();
            switch (selection.SelectedValue)
            {
                case 1:
                    var codingGoals = _codingController.GetAllCodingGoals();
                    if(codingGoals.Count != 0)
                    {
                        _visual.RenderCodingGoals(codingGoals);
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]NO CODING GOALS RECORDED!![/]");
                    }
                    Console.ReadLine();
                    break;
                case 2:
                    try
                    {
                        int rowAffected = SetGoals();
                        _visual.RenderResult(rowAffected);
                    }
                    catch (ExitOutOfOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 3:
                    codingGoals = _codingController.GetAllCodingGoals();
                    if(codingGoals.Count != 0)
                    {
                        _visual.RenderCodingGoals(codingGoals);
                        DeleteGoal(codingGoals);
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]NO CODING GOALS TO DELETE[/]");
                    }
                    break;
                case 0:
                    return;
            }
            Console.Clear();
        }
    }
    public int SetGoals()
    {

        AnsiConsole.Write(new Rule("[green bold]CODING GOAL[/]"));
        TimeSpan setTime = _UserInput.SetTimeDuration();
        Console.WriteLine();
        DateTime startingDate = _UserInput.GetStartDate();
        Console.WriteLine();
        int days = AnsiConsole.Ask<int>("Enter the number of days you need to complete your coding goals (eg. 1, 2, 11): ");

        Console.WriteLine();

        List<CodingSession> codingSessions = _codingController.GetAllData();

        List<CodingSession> codingGoalSessions = codingSessions
                .Where(c => c.Date.Date >= startingDate.Date
                    && c.Date.Date < startingDate.Date.AddDays(days)).ToList();


        //adding the totalTime user has coded , from starting date to today
        TimeSpan totalCodingTime = TimeSpan.Zero;

        foreach (var codingSession in codingGoalSessions)
        {
            totalCodingTime += codingSession.Duration;
        }

        //TimeSpan to representing time user need to complete
        TimeSpan diffInCodingTime = setTime - totalCodingTime;

        // days left to complete coding goal.
        int daysLeft = days - (DateTime.Now.DayOfYear - startingDate.DayOfYear);
        Console.Clear();
        AnsiConsole.Write(new Rule("[red bold]CODING GOAL REPORT[/]"));
        _visual.ShowReport("Time Completed", totalCodingTime.ToString(), "green");
        _visual.ShowReport("Time To Complete", diffInCodingTime.ToString(), "red");
        _visual.ShowReport("Days left to Complete", daysLeft.ToString(), "red");
        _visual.ShowReport("Average time you need to code to complete goal", new TimeSpan(diffInCodingTime.Ticks / daysLeft).ToString(), "green");
        var goal = new CodingGoals()
        {
           Time_to_complete = diffInCodingTime.ToString(),
            Avg_Time_To_Code = new TimeSpan(diffInCodingTime.Ticks / daysLeft).ToString(),
            Days_left = daysLeft
        };

        return _codingController.PostGoals(goal);

    }

    public void DeleteGoal(List<CodingGoals> codingGoals)
    {
        try
        {

            CodingGoals codingGoal = _UserInput.GetCodingGoal(codingGoals);
            int rowAffected = _codingController.DeleteGoal(codingGoal);
            _visual.RenderResult(rowAffected);
        }
        catch(ExitOutOfOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}
