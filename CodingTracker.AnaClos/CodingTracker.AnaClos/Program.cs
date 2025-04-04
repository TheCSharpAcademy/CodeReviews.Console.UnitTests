using AnaClos.CodingTracker;
using Microsoft.Data.Sqlite;
using Spectre.Console;

string choice = "q";
string? connectionString = System.Configuration.ConfigurationManager.AppSettings.Get("connectionString");
UserInput input = new UserInput();
CodingController controller = new CodingController();

CreateBaseAndTable(connectionString);

choice = input.Menu();

while (choice != "q")
{
    switch (choice)
    {
        case "i":
            controller.Insert(connectionString);
            break;
        case "v":
            controller.View(connectionString);
            AnsiConsole.Prompt(new TextPrompt<string>("[bold blue]\nPress a key to continue[/]\n"));
            break;
        case "d":
            controller.Delete(connectionString);
            break;
        case "u":
            controller.Update(connectionString);
            break;
        case "q":
            AnsiConsole.MarkupLine("[bold blue]By!!![/]\n");
            break;
    }
    choice = input.Menu();
}

void CreateBaseAndTable(string connectionString)
{
    using (var connection = new SqliteConnection(connectionString))
    {
        connection.Open();
        var tableCmd = connection.CreateCommand();

        tableCmd.CommandText =
            @"CREATE TABLE IF NOT EXISTS session (
            Id INTEGER PRIMARY KEY AUTOINCREMENT, 
            StartTime TEXT,
            EndTime TEXT
            )";

        tableCmd.ExecuteNonQuery();
        connection.Close();
    }
}