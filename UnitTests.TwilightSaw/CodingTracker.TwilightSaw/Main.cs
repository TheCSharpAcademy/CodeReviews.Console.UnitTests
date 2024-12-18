using CodingTracker.TwilightSaw;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
string appSettingsPath = Path.Combine(projectDirectory, "appsettings.json");
var builder = new ConfigurationBuilder().AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);

IConfiguration configuration = builder.Build();
string connectionString = configuration.GetConnectionString("DefaultConnection");

using var connection = new SqliteConnection(connectionString);
connection.Open();

var session = new CodingSession();
var controller = new TrackerController(connection);
var userInput = new UserInput();
var end = true;

while (end)
{
    var panelHeader = new Panel("[blue]Welcome to the Coding Tracker![/]").Padding(42, 3, 3, 3).Expand();
    AnsiConsole.Write(panelHeader);

    var input = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[blue]Please, choose an option from the list below:[/]")
            .PageSize(10)
            .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
            .AddChoices("Start a Coding Session.", "Add a Coding Session.", "Change an existed Coding Session.",
                "Show your Coding Sessions.", "Delete a Coding Session.", "Reports", "Goals", "Exit"));
    switch (input)
    {
        case "Start a Coding Session.":
            controller.CreateWithTimer(connection, session);
            break;
        case "Add a Coding Session.":
            var dateAddInput = userInput.CreateRegex(@"^([0-2][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$|^T|t$",
                "Type the date(dd.mm.yyyy) of your Coding Session or type T for today's date: "
                , "Wrong data format, try again. Example: 01.01.2001 or T for today's date: ");
            dateAddInput = UserInput.CheckT(dateAddInput);

            var startAddTimeInput = userInput.CreateRegex(@"^([0-1][0-9]|2[0-3])\:([0-5][0-9])\:([0-5][0-9])$",
                "Type your Coding Session start time(hh:mm:ss) "
                , "Wrong data format, try again. Example: 10:10:10: ");

            var endTimeAddInput = userInput.CreateRegex(@"^([0-1][0-9]|2[0-3])\:([0-5][0-9])\:([0-5][0-9])$|^N|n$",
                "Add end time(hh:mm:ss) of your Coding Session or type N for the time at this moment: "
                , "Wrong data format, try again. Example: 12:12:12 or N for the time at this moment: ");

            endTimeAddInput = UserInput.CheckN(endTimeAddInput);
            session.StartTime = DateTime.Parse(dateAddInput + " " + startAddTimeInput);
            session.EndTime = DateTime.Parse(dateAddInput + " " + endTimeAddInput);
            controller.Create(connection, session);
            break;
        case "Change an existed Coding Session.":
            var dateChangeInput = userInput.CreateRegex(@"^([0-2][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$|^T|t$",
                "Type the date(dd.mm.yyyy) of your Coding Session or type T for today's date: ",
                "Wrong data format, try again. Example: 01.01.2001 or T for today's date: ");
            dateChangeInput = UserInput.CheckT(dateChangeInput);

            var sessionTimeInput = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Please, choose an option from the list below:[/]")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more categories)[/]")
                    .AddChoices("Change Start Time", "Change End Time"));

            var addSessionList = controller.ReadBy(connection, dateChangeInput, "Empty date.");
            if (addSessionList.Count == 0) break;
            var chooseChangeSession = UserInput.ChooseSession(addSessionList);

            AnsiConsole.Write(new Rows(new Text($"\nSession to change - date {chooseChangeSession.Date}, " +
                                                $"Start Time - {chooseChangeSession.StartTime.ToLongTimeString()} " +
                                                $"and End Time = {chooseChangeSession.EndTime.ToLongTimeString()}", new Style(Color.Orange1))));

            var sessionTimeString = sessionTimeInput == "Change Start Time"
                ? "Type your new Start Coding time(hh:mm:ss): "
                : "Type your new End Coding time(hh:mm:ss): ";

            var changeTimeInput = userInput.CreateRegex(@"^([0-1][0-9]|2[0-3])\:([0-5][0-9])\:([0-5][0-9])$|^N|n$",
                sessionTimeString,
                "Wrong data format, try again. Example: 10:10:10: ");

            if (sessionTimeInput is "Change Start Time")
            {
                session.StartTime = DateTime.Parse(dateChangeInput + " " + changeTimeInput);
                controller.Update(connection, session, chooseChangeSession.StartTime.ToLongTimeString(),
                    sessionTimeInput);
            }
            else
            {
                session.EndTime = DateTime.Parse(dateChangeInput + " " + changeTimeInput);
                controller.Update(connection, session, chooseChangeSession.EndTime.ToLongTimeString(),
                    sessionTimeInput);
            }
            break;
        case "Show your Coding Sessions.":
            var data = controller.Read(connection);
            var endRead = true;
            while(endRead){
                var table = new Table();
                table.AddColumn("Date")
                    .AddColumn("Start of Session")
                    .AddColumn("End of Session")
                    .AddColumn("Total Session Duration").Centered();
                foreach (var x in data)
                    table.AddRow(@$"{x.Date}", $"{x.StartTime.ToLongTimeString()}", $"{x.EndTime.ToLongTimeString()}",
                        $"{x.Duration}");
                AnsiConsole.Write(table);

                var inputFilter = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Please, choose an option from the list below:[/]")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
                        .AddChoices("Filter per year", "Filter per month", "Filter per day", "Sort", "Exit"));
                switch (inputFilter)
                {
                    case "Filter per year":
                        var yearInput = userInput.CreateRegex(@"^(\d{4})$", "Insert desired year: ", "Wrong symbols.");
                        data = controller.ReadBy(connection, yearInput, "Empty date.");
                        Console.Clear();
                        break;
                    case "Filter per month":
                        var monthInput = userInput.CreateRegex(@"^(0[1-9]|1[0-2])\.(\d{4})$", "Insert desired mm.year: ", "Wrong symbols.");
                        data = controller.ReadBy(connection, monthInput, "Empty date.");
                        Console.Clear();
                        break;
                    case "Filter per day":
                        var dayInput = userInput.CreateRegex(@"^([0-2][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$", "Insert desired dd.mm.year: ", "Wrong symbols.");
                        data = controller.ReadBy(connection, dayInput, "Empty date.");
                        Console.Clear();
                        break;
                    case "Sort":
                        var inputOrder = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                                .Title("[blue]Please, choose an option from the list below:[/]")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more categories[/]")
                                .AddChoices("Descending", "Ascending"));
                        data = controller.Order(connection, data, inputOrder == "Ascending");
                        Console.Clear();
                        break;
                    case "Exit":
                        endRead = false;
                        break;
                }
            }
            break;
        case "Delete a Coding Session.":
            var deleteDateInput = userInput.CreateRegex(@"^([0-2][0-9]|3[01])\.(0[1-9]|1[0-2])\.(\d{4})$|^T|t$",
                "Type the data(dd.mm.yyyy) of your Coding Session you chose to delete or type T for today's date: ",
                "Wrong data format, try again. Example: 01.01.2001 or T for today's date: ");
            deleteDateInput = UserInput.CheckT(deleteDateInput);

            var deleteSessionList = controller.ReadBy(connection, deleteDateInput, "Empty date.");
            if (deleteSessionList.Count == 0) break;
            var chooseDeleteSession = UserInput.ChooseSession(deleteSessionList);

            AnsiConsole.Write(new Rows(new Text($"\nSession to delete - date {chooseDeleteSession.Date}, " +
                                                $"Start Time - {chooseDeleteSession.StartTime.ToLongTimeString()} " +
                                                $"and End Time = {chooseDeleteSession.EndTime.ToLongTimeString()}", new Style(Color.Orange1))));
            controller.Delete(connection, deleteDateInput, chooseDeleteSession.StartTime.ToLongTimeString());
            
            break;
        case "Reports":
            var reportData = controller.CreateReport(connection);
            AnsiConsole.Write(new Columns(
                new Text($"You have achieved {reportData.Item1} sessions.", new Style(Color.Orange1)),
                new Text($"Total time you spent coding is {reportData.Item2}", new Style(Color.Orange1)),
                new Text($"Your average session lasts {reportData.Item3}", new Style(Color.Orange1))));
            break;
        case "Goals":
            var reportGoalData = controller.CreateReport(connection);
            Console.Write("What is your desired amount of coding hours in a day: ");
            var goalInput = userInput.CreateSpecifiedInt(12, "Only reachable amount of time: ");
            var goalHours = reportGoalData.Item3.TotalHours;

            AnsiConsole.Write(new BarChart()
                .Width(60)
                .Label("[green bold underline]Average time per day[/]")
                .CenterLabel()
                .AddItem("Your Time", Math.Round(goalHours, 1), Color.Red)
                .AddItem("Planned Time", Convert.ToDouble(goalInput), Color.Green));
            Console.WriteLine(goalHours > goalInput
                ? $"You are ahead of plan for {goalHours - goalInput:F1} hours! Congrats!"
                : $"You need {goalInput - goalHours:F1} hours more to achieve your goal! Keep going!");
            break;
        case "Exit":
            end = false;
            break;
        default:
            Console.WriteLine("Wrong choice selection, try again! \n");
            break;
    }
    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    Console.Clear();
}