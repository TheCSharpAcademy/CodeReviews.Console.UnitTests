using CodingTracker.Models;
using Spectre.Console;

namespace CodingTracker.Controllers
{
    internal class UserInputs
    {
        public static string GetStringInput(string message = "")
        {
            string? readResult = "";
            bool validInput = false;
            while (!validInput)
            {
                readResult = AnsiConsole.Prompt(
                    new TextPrompt<string>(message));
                validInput = Validators.ValidateString(readResult);
            }
            return readResult;
        }

        public static string GetDateTimeInput(string message, string type = "Date")
        {
            bool validInput = false;
            string readResult = "";
            do
            {
                if (type == "Date")
                {
                    while (!validInput)
                    {
                        readResult = AnsiConsole.Prompt(
                            new TextPrompt<string>(message));
                        validInput = Validators.ValidateDate(readResult);
                    }
                    return readResult;
                }
                else
                {
                    while (!validInput)
                    {
                        readResult = AnsiConsole.Prompt(
                            new TextPrompt<string>(message));
                        validInput = Validators.ValidateTime(readResult);
                    }
                    return readResult;
                }
            }
            while (!validInput);
        }

        public static string GetDurationEstimation()
        {
            bool validInput = false;
            TimeSpan durationEstimation = new();
            string readResult;
            while (!validInput)
            {
                readResult = AnsiConsole.Prompt(
                    new TextPrompt<string>("Please enter a time estimation: (d.HH:MM.ss)"));
                validInput = Validators.ValidateDurationEstimation(readResult);
            }
            return durationEstimation.ToString();
        }

        public static string SelectExistingProject(bool newProject = false, bool Goal = false)
        {
            SelectionPrompt<string> prompt = new();
            List<string> projects = DataTools.GetTables();
            prompt.AddChoice("Cancel");
            if (newProject) prompt.AddChoice("Add new Project");

            if (projects != null)
            {
                if (Goal)
                {
                    foreach (string project in projects)
                    {
                        if (project.IndexOf("_CGoal") != -1) prompt.AddChoice(project);
                    }
                }
                else
                {
                    foreach (string project in projects)
                    {
                        if (project.IndexOf("_CGoal") == -1) prompt.AddChoice(project);
                    }
                }
            }

            prompt.Title("Select a Project:");
            prompt.WrapAround(true);
            string? selected = AnsiConsole.Prompt(
                prompt
                );
            return selected == "Cancel" ? null : selected;
        }

        public static CodingSession GetSpecificData(List<CodingSession> datas)
        {
            try
            {
                SelectionPrompt<CodingSession> prompt = new();
                if (datas != null)
                {
                    prompt.AddChoice(new CodingSession { StartDate = "Cancel" });
                    foreach (CodingSession data in datas)
                    {
                        prompt.AddChoice(data);
                        prompt.Converter = data => $"{data.StartDate} {data.StartTime} | {data.EndDate} {data.EndTime} | {data.Duration}";
                    }
                }
                prompt.Title("Select a Data:");
                prompt.WrapAround(true);
                CodingSession selected = AnsiConsole.Prompt(
                    prompt
                    );
                if (selected.StartDate != "Cancel") return selected;
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine(ex.Message);
                Console.Read();
            }
            return null;
        }

        public static List<string> GetMultipeData(List<CodingSession> datas)
        {
            try
            {
                MultiSelectionPrompt<CodingSession> prompt = new();
                if (datas != null)
                {
                    prompt.AddChoice(new CodingSession { StartDate = "Cancel" });
                    foreach (CodingSession data in datas)
                    {
                        prompt.AddChoice(data);
                        prompt.Converter = data => $"{data.StartDate} {data.StartTime} | {data.EndDate} {data.EndTime} | {data.Duration}";
                    }
                }
                prompt.Title("Select data(s)");
                prompt.WrapAround(true);
                List<CodingSession> selected = AnsiConsole.Prompt(prompt);
                List<string> selectedDataIds = new();
                foreach (CodingSession data in selected)
                {
                    selectedDataIds.Add(data.Rowid.ToString());
                }
                if (!selectedDataIds.Contains("0")) return selectedDataIds;
            }
            catch (Exception ex) { AnsiConsole.Markup(ex.Message); }
            return null;
        }

        public static bool ValidateInput(string message = "", bool defVal = true, string choice1 = "y", string choice2 = "n")
        {
            bool validation = AnsiConsole.Prompt(
                                new TextPrompt<bool>(message)
                                .AddChoice(true)
                                .AddChoice(false)
                                .DefaultValue(defVal)
                                .WithConverter(choice => choice ? choice1 : choice2));
            return validation;
        }

        public static string SelectAscDesc()
        {
            SelectionPrompt<string> prompt = new SelectionPrompt<string>();
            prompt.Title("Select output order:");
            prompt.AddChoices("Ascendant", "Descendant");
            return AnsiConsole.Prompt(prompt).Substring(0, 3);
        }
    }
}