using CodingTracker.Models;
using CodingTracker.Views;
using Spectre.Console;
using System.Configuration;

namespace CodingTracker.Controllers
{
    internal class MenuActions
    {
        internal MenuModel menuModel { get; set; }

        internal MenuActions()
        {
            menuModel = new();
            SetMenuModel();
        }

        internal string SelectOption()
        {
            return AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("Choose an [yellow bold]option[/] below:")
                    .AddChoices("Start/End new coding session", "Insert new data", "Delete data",
                    "Update data", "Delete project", "Print single project report", "Print all data", "Set/Show Coding Goals", "See current session duration", "Exit", "Fill database for testing purpose")
                    .WrapAround(true)
                    );
        }

        internal void EndCurrentSession()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            menuModel.IsCodingSessionRunning = false;
            config.AppSettings.Settings["newCodingSession"].Value = "false";

            menuModel.StartDate = config.AppSettings.Settings["currentStartDate"].Value;
            menuModel.StartTime = config.AppSettings.Settings["currentStarttime"].Value;
            menuModel.EndDate = DateTime.Now.ToString("yyyy.MM.dd");
            menuModel.EndTime = DateTime.Now.ToString("HH:mm");
            menuModel.Duration = DataTools.GetDuration(menuModel.StartDate, menuModel.StartTime, menuModel.EndDate, menuModel.EndTime);

            menuModel.SqlCommandText = $"INSERT INTO {menuModel.CurrentCodingSession} (StartDate,EndDate,StartTime,EndTime,Duration) VALUES($startDate,$endDate,$startTime,$endTime,$duration)";
            DataTools.ExecuteQuery(menuModel.SqlCommandText, startDate: menuModel.StartDate, endDate: menuModel.EndDate, startTime: menuModel.StartTime, endTime: menuModel.EndTime, duration: menuModel.Duration);
            
            config.Save(ConfigurationSaveMode.Full);
        }

        internal void BeginNewCodingSession()
        {
            menuModel.IsCodingSessionRunning = UserInputs.ValidateInput("Begin a new coding session?");
            
            if (menuModel.IsCodingSessionRunning)
            {
                menuModel.Project = UserInputs.SelectExistingProject(newProject: true);
                if (menuModel.Project != null)
                {
                    if (menuModel.Project == "Add new Project")
                    {
                        menuModel.Project = UserInputs.GetStringInput("Enter project [blue]name[/] (only [red]letter,numbers and '_'[/] are authorized):");
                        DataTools.CreateNewTable(menuModel.Project);
                    }
                    menuModel.CurrentCodingSession = menuModel.Project;

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    config.AppSettings.Settings["newCodingSession"].Value = "true";
                    config.AppSettings.Settings["currentCodingSession"].Value = menuModel.CurrentCodingSession;
                    config.AppSettings.Settings["currentStartDate"].Value = DateTime.Now.ToString("yyyy.MM.dd");
                    config.AppSettings.Settings["currentStartTime"].Value = DateTime.Now.ToString("HH:mm");
                    config.Save(ConfigurationSaveMode.Full);

                    AnsiConsole.MarkupLine($"A new coding session has begun for [blue]{menuModel.CurrentCodingSession}[/]");
                    Console.ReadKey();
                }
                else { menuModel.IsCodingSessionRunning = false; }
            }
            else { menuModel.IsCodingSessionRunning = false; }
        }

        internal void InsertNewData()
        {
            menuModel.Project = UserInputs.SelectExistingProject(newProject: true);
            if (menuModel.Project != null)
            {
                if (menuModel.Project == "Add new Project")
                {
                    menuModel.Project = UserInputs.GetStringInput("Enter project [blue]name[/] (only [red]letter,numbers and '_'[/] are authorized):");
                    DataTools.CreateNewTable(menuModel.Project);
                }
                menuModel.StartDate = UserInputs.GetDateTimeInput($"Pease enter a starting date for {menuModel.Project} (format [green]yyyy.MM.dd[/]):");
                menuModel.StartTime = UserInputs.GetDateTimeInput($"Pease enter a starting time for {menuModel.Project} (format [green]HH:mm[/]):", type: "Time");
                menuModel.EndDate = UserInputs.GetDateTimeInput($"Pease enter an ending date for {menuModel.Project} (format [green]yyyy.MM.dd[/]):");
                menuModel.EndTime = UserInputs.GetDateTimeInput($"Pease enter an ending time for {menuModel.Project} (format [green]HH:mm[/]):", type: "Time");
                menuModel.Duration = DataTools.GetDuration(menuModel.StartDate, menuModel.StartTime, menuModel.EndDate, menuModel.EndTime);
                menuModel.SqlCommandText = $"INSERT INTO {menuModel.Project} (StartDate,EndDate,StartTime,EndTime,Duration) VALUES($startDate,$endDate,$startTime,$endTime,$duration)";
                DataTools.ExecuteQuery(menuModel.SqlCommandText, startDate: menuModel.StartDate, endDate: menuModel.EndDate, startTime: menuModel.StartTime, endTime: menuModel.EndTime, duration: menuModel.Duration);
            }
        }
        internal void DeleteData()
        {
            menuModel.Project = UserInputs.SelectExistingProject();
            if (menuModel.Project != null)
            {
                List<CodingSession> projectData = DataTools.GetProjectData(menuModel.Project);
                if (projectData != null)
                {
                    List<string> selectedDatas = UserInputs.GetMultipeData(projectData);
                    if (selectedDatas != null)
                    {
                        foreach (string id in selectedDatas)
                        {
                            menuModel.SqlCommandText = $"DELETE FROM {menuModel.Project} WHERE rowid = $id";
                            DataTools.ExecuteQuery(menuModel.SqlCommandText, id: id);
                        }
                    }
                }
                else
                {
                    Console.ReadKey();
                }
            }
        }

        internal void UpdateData()
        {
            menuModel.Project = UserInputs.SelectExistingProject();
            if (menuModel.Project != null)
            {
                List<CodingSession> projectData = DataTools.GetProjectData(menuModel.Project);
                if (projectData != null)
                {
                    menuModel.CurrentData = UserInputs.GetSpecificData(projectData);
                    if (menuModel.CurrentData != null)
                    {
                        string dataId = menuModel.CurrentData.rowid.ToString();
                        string dataToModify = AnsiConsole.Prompt(
                            new SelectionPrompt<string>().AddChoices("Start Date", "Start Time", "End Date", "End Time"));
                        string modifiedData = "";
                        bool validInput = false;
                        switch (dataToModify)
                        {
                            case "Start Date":
                                while (!validInput)
                                {
                                    modifiedData = UserInputs.GetDateTimeInput($"Pease enter a starting date to replace (format [green]yyyy.MM.dd[/]):");
                                    validInput = Validators.ValidateStartAndEnd(modifiedData,$"{menuModel.CurrentData.EndDate} {menuModel.EndTime}");
                                    if (!validInput) AnsiConsole.MarkupLine($"New date must be before {menuModel.EndDate} {menuModel.EndTime}");
                                }
                                menuModel.SqlCommandText = $"UPDATE {menuModel.Project} SET StartDate = $startDate , Duration = $duration WHERE rowid = $id ";
                                menuModel.Duration = DataTools.GetDuration(modifiedData, menuModel.CurrentData.StartTime, menuModel.CurrentData.EndDate, menuModel.CurrentData.EndTime);
                                DataTools.ExecuteQuery(menuModel.SqlCommandText, startDate: modifiedData, duration: menuModel.Duration, id: dataId);
                                break;

                            case "Start Time":
                                modifiedData = UserInputs.GetDateTimeInput($"Pease enter a starting time to replace (format [green]HH:mm[/]):", type: "Time");
                                menuModel.SqlCommandText = $"UPDATE {menuModel.Project} SET StartTime = $startTime , Duration = $duration WHERE rowid = $id ";
                                menuModel.Duration = DataTools.GetDuration(menuModel.CurrentData.StartDate, modifiedData, menuModel.CurrentData.EndDate, menuModel.CurrentData.EndTime);
                                DataTools.ExecuteQuery(menuModel.SqlCommandText, startTime: modifiedData, duration: menuModel.Duration, id: dataId);
                                break;

                            case "End Date":
                                modifiedData = UserInputs.GetDateTimeInput($"Pease enter an ending date to replace (format [green]yyyy.MM.dd[/]):");
                                menuModel.SqlCommandText = $"UPDATE {menuModel.Project} SET EndDate = $endDate , Duration = $duration WHERE rowid = $id ";
                                menuModel.Duration = DataTools.GetDuration(menuModel.CurrentData.StartDate, menuModel.CurrentData.StartTime, modifiedData, menuModel.CurrentData.EndTime);
                                DataTools.ExecuteQuery(menuModel.SqlCommandText, endDate: modifiedData, duration: menuModel.Duration, id: dataId);
                                break;

                            case "End Time":
                                modifiedData = UserInputs.GetDateTimeInput($"Pease enter an ending time to replace (format [green]HH:mm[/]):", type: "Time");
                                menuModel.SqlCommandText = $"UPDATE {menuModel.Project} SET EndTime = $endTime , Duration = $duration WHERE rowid = $id";
                                menuModel.Duration = DataTools.GetDuration(menuModel.CurrentData.StartDate, menuModel.CurrentData.StartTime, menuModel.CurrentData.EndDate, modifiedData);
                                DataTools.ExecuteQuery(menuModel.SqlCommandText, endTime: modifiedData, duration: menuModel.Duration, id: dataId);
                                break;
                        }
                    }
                }
                else Console.ReadKey();
            }
        }

        internal void DeleteProject()
        {
            string projectType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Choose a type to delete: ")
                .AddChoices("Normal Project", "Goal Project"));

            if (projectType == "Normal Project") { menuModel.Project = UserInputs.SelectExistingProject(); }
            else menuModel.Project = UserInputs.SelectExistingProject(Goal: true);
            
            if (menuModel.Project != null)
            {
                bool validation = UserInputs.ValidateInput($"Are you sure you want to delete [red]{menuModel.Project}[/]?", defVal: false);
                if (validation)
                {
                    menuModel.SqlCommandText = $"DROP TABLE {menuModel.Project}";
                    DataTools.ExecuteQuery(menuModel.SqlCommandText);
                }
            }
        }

        internal void PrintSingleProjectReport()
        {
            menuModel.Project = UserInputs.SelectExistingProject();
            if (menuModel.Project != null)
            {
                SelectionPrompt<string> prompt = new();

                // By getting total duration through sqlite, time calculation made by it can go off if reports are other than weekly(see DataOutput)
                prompt.Title("What kind of report do you need? (For more precise total duration report, select 'Weekly')");
                prompt.AddChoices("All data", "Weekly", "Monthly", "Yearly");
                string option = AnsiConsole.Prompt(prompt);
                string ascDesc = UserInputs.SelectAscDesc();
                DataOutput.PrintProjectData(menuModel.Project, ascDesc, option);
                Console.ReadKey();
            }
        }

        internal void PrintAllData()
        {
            string ascDesc = UserInputs.SelectAscDesc();
            List<string> allProjects = DataTools.GetTables();
            AnsiConsole.Clear();
            if (allProjects != null)
            {
                foreach (string project in allProjects) DataOutput.PrintProjectData(project, ascDesc);
            }
            else
            {
                AnsiConsole.MarkupLine("There is [red bold]no[/] project in the data base");
            }
            Console.ReadKey();
        }

        internal void SeeShowGoals()
        {
            string setSee = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .AddChoices("Set", "Show"));
            if (setSee == "Set")
            {
                menuModel.Project = UserInputs.GetStringInput("Enter a project name(_CGoal suffix will be add to it's name): ");
                DataTools.SetGoals(menuModel.Project);
            }
            else
            {
                menuModel.Project = UserInputs.SelectExistingProject(Goal: true);
                if (menuModel.Project != null) { DataOutput.ShowGoal(DataTools.GetGoalToShow(menuModel.Project)); }
            }
            Console.ReadKey();
        }

        internal void ShowCurrentSession()
        {
            if (menuModel.IsCodingSessionRunning)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                AnsiConsole.MarkupLine($"{DataTools.GetDuration(config.AppSettings.Settings["currentStartDate"].Value, config.AppSettings.Settings["currentStartTime"].Value, DateTime.Now.ToString("yyyy.MM.dd"), DateTime.Now.ToString("HH:mm"))}");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]No[/] coding session running.");
            }
            Console.ReadKey();
        }

        internal void SetMenuModel()
        {

            Configuration config1 = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            bool isSessionRunning;
            bool.TryParse(config1.AppSettings.Settings["newCodingSession"].Value, out isSessionRunning);
            menuModel.IsCodingSessionRunning = isSessionRunning;
            menuModel.CurrentCodingSession = config1.AppSettings.Settings["currentCodingSession"].Value;
        }
    }
}