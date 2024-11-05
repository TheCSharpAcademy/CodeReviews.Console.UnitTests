using CodingTracker.Arashi256.Classes;
using CodingTracker.Arashi256.Controllers;
using CodingTracker.Arashi256.Models;
using Spectre.Console;

namespace CodingTracker.Arashi256.Views
{
    internal class MainView
    {
        private const int QUIT_APPLICATION_OPTION_NUM = 9;
        private Table _tblMainMenu;
        private string _appTitle = "[CODING TRACKER]";
        private FigletText _figletAppTitle;
        private SessionController? _sessionController;
        private CodingGoalView? _codingGoalView;
        private CodingReportsView? _codingReportsView;
        private string[] _menuOptions =
        {
            "Add a new coding session",
            "Update existing coding session",
            "Delete existing coding session", 
            "List last 7 days coding sessions",
            "Codng session Reports",
            "Live coding session",
            "Coding goals",
            "Reset database",
            "Exit application"
        };

        public MainView()
        {
            _figletAppTitle = new FigletText(_appTitle);
            _figletAppTitle.Centered();
            _figletAppTitle.Color = Color.Aqua;
            _tblMainMenu = new Table();
            _tblMainMenu.AddColumn(new TableColumn("[white]CHOICE[/]").Centered());
            _tblMainMenu.AddColumn(new TableColumn("[white]OPTION[/]").LeftAligned());
            for (int i = 0; i < _menuOptions.Length; i++)
            {
                _tblMainMenu.AddRow($"[lightcyan3]{i + 1}[/]", $"[darkslategray1]{_menuOptions[i]}[/]");
            }
            _tblMainMenu.Alignment(Justify.Center);
            InitControllers();
            InitSubViews();
        }

        private void InitControllers()
        {
            _sessionController = new SessionController();
        }

        private void InitSubViews()
        {
            _codingGoalView = new CodingGoalView(_sessionController);
            _codingReportsView = new CodingReportsView(_sessionController);
        }

        public void DisplayMainMenu()
        {
            int selectedValue = 0;
            do
            {
                Console.Clear();
                AnsiConsole.Write(_figletAppTitle);
                AnsiConsole.Write(new Text("M A I N   M E N U").Centered());
                AnsiConsole.Write(_tblMainMenu);
                selectedValue = CommonUI.MenuOption($"Enter a value between 1 and {_menuOptions.Length}: ", 1, _menuOptions.Length);
                ProcessMainMenu(selectedValue);
            } while (selectedValue != QUIT_APPLICATION_OPTION_NUM);
            AnsiConsole.MarkupLine("[lime]Goodbye![/]");
        }

        private void ProcessMainMenu(int option)
        {
            AnsiConsole.Markup($"[white]Menu option selected: {option}[/]\n");
            switch (option)
            {
                case 1:
                    AddNewCodingSession();
                    break;
                case 2:
                    UpdateExistingCodingSession();
                    break;
                case 3:
                    DeleteExistingCodingSession();
                    break;
                case 4:
                    ListLastCodingSessions(7, Utility.SortOrder.ASC);
                    CommonUI.Pause();
                    break;
                case 5:
                    if (_codingReportsView != null)
                        _codingReportsView.CodingSessionReports();
                    else
                        AnsiConsole.Markup("[red]Reports view is not initialized.[/]\n");
                    break;
                case 6:
                    LiveCodingSession();
                    break;
                case 7:
                    if (_codingGoalView != null)
                        _codingGoalView.CodingGoalMenu();
                    else
                        AnsiConsole.Markup("[red]Goals view is not initialized.[/]\n");
                    break;
                case 8:
                    ResetDatabase();
                    break;
            }
        }


        private void LiveCodingSession()
        {
            AnsiConsole.MarkupLine("[cyan1]Live coding session - this will activate a timer for your coding session[/]");
            AnsiConsole.MarkupLine("[white]Once the timer is running, press any key to stop the session.[/]");
            AnsiConsole.MarkupLine("[white]After which time you will have the option to save or discard the session.[/]");
            CommonUI.Pause();
            Console.Clear();
            var liveSession = new LiveSession();
            liveSession.StopwatchStopped += OnLiveSessionStopped;
            liveSession.Start();
            Console.ReadKey(intercept: true);
            liveSession.Stop();
        }

        private void OnLiveSessionStopped(DateTime startTime, DateTime endTime, TimeSpan elapsedTime)
        {
            AnsiConsole.MarkupLine($"[skyblue2]Live coding session started at:[/]\t[skyblue1]{startTime:dd-MM-yy HH:mm:ss}[/]");
            AnsiConsole.MarkupLine($"[skyblue2]Live coding session stopped at:[/]\t[skyblue1]{endTime:dd-MM-yy HH:mm:ss}[/]");
            AnsiConsole.MarkupLine($"[skyblue2]Elapsed Time:[/]\t\t\t[cyan1]{elapsedTime:hh\\:mm\\:ss}[/]");
            if (AnsiConsole.Confirm("\n[yellow]Do you want to save this coding session?[/]", false))
            {
                if (_sessionController != null)
                {
                    string result = _sessionController.AddCodingSession(startTime, endTime);
                    AnsiConsole.MarkupLine($"[paleturquoise1]{result}[/].\n");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error: Session controller is not initialized. Cannot save coding session.[/]\n");
                }
            }
            CommonUI.Pause();
        }

        private void ResetDatabase()
        {
            if (_sessionController == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Session controller is not initialized. Cannot reset the database.[/]\n");
                return;
            }
            AnsiConsole.MarkupLine("[white]Reset database[/]");
            AnsiConsole.MarkupLine("[white]This will reset the database and return to factory settings.[/]");
            AnsiConsole.MarkupLine("[red]WARNING: Performing this operation will result in all current data being deleted. This data cannot be retrieved![/]");
            if (AnsiConsole.Confirm("[yellow]Are you sure you want to continue?[/]", false))
            {
                AnsiConsole.MarkupLine("[lime]Resetting database...please wait[/]");
                if (_sessionController.ResetDatabase())
                {
                    AnsiConsole.MarkupLine("[lime]SUCCESS:[/] [white]Database reset to initial settings.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]ERROR:[/] [white]Database could not be reset. Please verify and try again.[/]");
                }
            }
            CommonUI.Pause();
        }

        private void AddNewCodingSession()
        {
            if (_sessionController == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Session controller is not initialized. Cannot add a new coding session.[/]\n");
                return;
            }
            DateTime? startTime, endTime;
            AnsiConsole.MarkupLine("[white]Enter a new coding session[/]");
            AnsiConsole.MarkupLine($"[white]Enter the date in 'dd-MM-yy' and then a space and 'HH:mm:ss' format for the clock time in 24H format for your coding session[/]");
            DateTime?[]? userDates = CommonUI.GetUserCodingSessionDatesDialog();
            if (userDates != null)
            {
                startTime = userDates[0];
                endTime = userDates[1];
                if (startTime.HasValue && endTime.HasValue)
                {
                    string result = _sessionController.AddCodingSession(startTime.Value, endTime.Value);
                    AnsiConsole.MarkupLine($"[paleturquoise1]{result}[/].\n");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error: Start and end times must be provided to add a coding session.[/]\n");
                }
                CommonUI.Pause();
            }
        }

        private void UpdateExistingCodingSession()
        {
            if (_sessionController == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Session controller is not initialized. Cannot update coding sessions.[/]\n");
                return;
            }
            int updateSessionID = 0;
            List<CodingSession> codingSessions = ListLastCodingSessions(7, Utility.SortOrder.ASC);
            if (codingSessions != null && codingSessions.Count > 0)
            {
                int[] validIds = _sessionController.GetArrayOfValidSessionIDs(codingSessions);
                if (AnsiConsole.Confirm("Here are the coding sessions for the last 7 days. Do you want to update one of these?"))
                {
                    updateSessionID = ChooseValidSessionIdDialog(validIds);
                }
                else
                {
                    updateSessionID = SearchForCodingSessionIDDialog();
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error: No sessions available to update.[/]\n");
                CommonUI.Pause();
                return;
            }
            // Ensure the selected session ID is within valid range
            if (updateSessionID > 0 && updateSessionID <= codingSessions.Count)
            {
                AnsiConsole.MarkupLine($"You selected session ID: [yellow]{updateSessionID}[/] to update");
                CodingSession selectedSession = codingSessions[updateSessionID - 1];
                // Check if selectedSession and its ID are not null before proceeding
                if (selectedSession?.Id != null)
                {
                    CodingSession? sessionToUpdate = _sessionController.GetCodingSession((int)selectedSession.Id);
                    if (sessionToUpdate != null)
                    {
                        DisplayCodingSession(sessionToUpdate);
                        DateTime?[]? updatedDateTimes = CommonUI.GetUserCodingSessionDatesDialog();
                        if (updatedDateTimes != null && updatedDateTimes.Length == 2 && updatedDateTimes[0].HasValue && updatedDateTimes[1].HasValue)
                        {
                            string result = _sessionController.UpdateCodingSession((int)sessionToUpdate.Id, updatedDateTimes[0].Value, updatedDateTimes[1].Value);
                            AnsiConsole.MarkupLine($"\n[lime]{result}[/]\n");
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Invalid date range provided. Please try again.[/]");
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"\n[red]There was a problem getting the coding session with ID: {updateSessionID}, please try again.[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Error: The selected session has an invalid ID.[/]");
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"\n[red]Invalid session ID: {updateSessionID}. Please select a valid session to update.[/]");
            }
            CommonUI.Pause();
        }


        private void DeleteExistingCodingSession()
        {
            if (_sessionController == null)
            {
                AnsiConsole.MarkupLine("[red]Error: Session controller is not initialized. Cannot update coding sessions.[/]\n");
                return;
            }
            int deleteSessionID = 0;
            List<CodingSession> codingSessions = ListLastCodingSessions(7, Utility.SortOrder.ASC);
            if (codingSessions != null && codingSessions.Count > 0)
            {
                int[] validIds = _sessionController.GetArrayOfValidSessionIDs(codingSessions);
                if (AnsiConsole.Confirm("Here are the coding sessions for the last 7 days. Do you want to delete one of these?"))
                {
                    deleteSessionID = ChooseValidSessionIdDialog(validIds);
                }
                else
                {
                    deleteSessionID = SearchForCodingSessionIDDialog();
                }
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Error: No sessions available to delete.[/]\n");
                CommonUI.Pause();
                return;
            }
            if (deleteSessionID != 0)
            {
                AnsiConsole.MarkupLine($"You selected session ID: [yellow]{deleteSessionID}[/] to delete");
                CodingSession? sessionToDelete = _sessionController.GetCodingSession((int)codingSessions[deleteSessionID - 1].Id);
                if (sessionToDelete != null)
                {
                    DisplayCodingSession(sessionToDelete);
                    AnsiConsole.MarkupLine($"\n[red]WARNING: Deleting a session cannot be undone![/]");
                    if (AnsiConsole.Confirm("Do you want to delete this record?"))
                    {
                        string result = _sessionController.DeleteCodingSession((int)sessionToDelete.Id);
                        AnsiConsole.MarkupLine($"\n[lime]{result}[/]\n");
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine($"\n[red]There was a problem getting the coding session with ID: {deleteSessionID}, please try again.[/]");
                }
            }
            CommonUI.Pause();
        }

        private int SearchForCodingSessionIDDialog()
        {
            int sessionID = 0;
            List<CodingSession> codingSessions;
            DateTime? startTime, endTime;
            AnsiConsole.MarkupLine($"[white]This will get all coding sessions between 2 dates you specify[/]");
            AnsiConsole.MarkupLine($"[white]Enter the dates in 'dd-MM-yy' and then a space and 'HH:mm:ss' format for the clock time in 24H format[/]");
            DateTime?[]? userDates = CommonUI.GetUserCodingSessionDatesDialog();
            if (userDates != null)
            {
                startTime = userDates[0];
                endTime = userDates[1];
                if (startTime != null && endTime != null)
                {
                    Utility.SortOrder sortOrder = CommonUI.GetSortOrderDialog();
                    codingSessions = _sessionController.GetCodingSessionsBetweenDates((DateTime)startTime, (DateTime)endTime, sortOrder);
                    int[] validIds = _sessionController.GetArrayOfValidSessionIDs(codingSessions);
                    CommonUI.DisplayCodingSessions(codingSessions);
                    sessionID = ChooseValidSessionIdDialog(validIds);
                }
                else
                {
                    sessionID = 0;
                }
            }
            else
            {
                sessionID = 0;
            }
            return sessionID;
        }

        private int ChooseValidSessionIdDialog(int[] validIds)
        {
            int selectedId = AnsiConsole.Prompt(
            new TextPrompt<int>("Please choose a session ID or enter '0' to cancel: ")
                .PromptStyle("lime")
                .ValidationErrorMessage("[red]That's not a valid session ID[/]")
                .Validate(id =>
                {
                    return id == 0 || validIds.Contains(id) ? ValidationResult.Success() : ValidationResult.Error("[red]Invalid session ID[/]");
                }));
            return selectedId;
        }

        

        private List<CodingSession> ListLastCodingSessions(int days, Utility.SortOrder sortOrder)
        {
            List<CodingSession> codingSessions = _sessionController.GetDaysCodingSessions(days, sortOrder);
            CommonUI.DisplayCodingSessions(codingSessions);
            return codingSessions;
        }

        private void DisplayCodingSession(CodingSession codingSession)
        {
            Table tblSession = new Table();
            tblSession.AddColumn(new TableColumn($"[skyblue1]ID[/]").LeftAligned());
            tblSession.AddColumn(new TableColumn($"[white]{codingSession.Id}[/]").RightAligned());
            tblSession.AddRow($"[skyblue1]Start Date/Time[/]", $"[white]{codingSession.StartDateTime:dd-MM-yy HH:mm:ss}[/]");
            tblSession.AddRow($"[skyblue1]End Date/Time[/]", $"[white]{codingSession.EndDateTime:dd-MM-yy HH:mm:ss}[/]");
            tblSession.AddRow($"[skyblue1]Duration[/]", $"[white]{codingSession.Duration:HH:mm:ss}[/]");
            tblSession.Alignment(Justify.Left);
            AnsiConsole.Write(tblSession);
        }
    }
}