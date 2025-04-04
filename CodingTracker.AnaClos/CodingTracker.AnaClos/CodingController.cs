using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

namespace AnaClos.CodingTracker;

public class CodingController
{
    public int ExecuteQuery(string connectionString, string sql, CodingSession session)
    {
        int rowsAffected;
        using (var connection = new SqliteConnection(connectionString))
        {
            rowsAffected = connection.Execute(sql, session);
        }
        return rowsAffected;
    }

    public void Insert(string connectionString)
    {
        string startTime = string.Empty;
        string endTime = string.Empty;
        string response = string.Empty;
        bool ok = false;
        UserInput input = new UserInput();
        Validation validation = new Validation();

        while (!ok)
        {
            Console.WriteLine();
            startTime = input.GetInput("Enter the Start Time in format dd/MM/yy HH:mm:ss.");
            ok = validation.ValidateDate(startTime);
        }
        if (startTime == "r")
        {
            return;
        }
        ok = false;
        while (!ok)
        {
            Console.WriteLine();
            endTime = input.GetInput("Enter the End Time in format dd/MM/yy HH:mm:ss.");
            ok = validation.ValidateDate(endTime);
            if(ok)
            {
                ok = validation.Validate2Dates(startTime, endTime);
            }
        }

        if (response == "r")
        {
            return;
        }

        var mySession = new CodingSession { StartTime = startTime, EndTime = endTime };
        var sql = "INSERT INTO session (StartTime,EndTime) VALUES (@StartTime,@EndTime)";
        ExecuteQuery(connectionString, sql, mySession);        
    }

    public void Update(string connectionString)
    {
        string startTime = string.Empty;
        string endTime = string.Empty;
        string response = string.Empty;
        int id = 0;
        bool ok = false;
        UserInput input = new UserInput();
        Validation validation = new Validation();

        View(connectionString);

        while (!ok)
        {
            Console.WriteLine();
            response = input.GetInput("Enter the Id to Update.");
            ok = validation.ValidateInt(response);
        }
        if (response == "r")
        {
            return;
        }

        id = Int32.Parse(response);
        ok = false;

        while (!ok)
        {
            Console.WriteLine();
            startTime = input.GetInput("Enter the Start Time in format dd/MM/yy HH:mm:ss.");
            ok = validation.ValidateDate(startTime);
        } 

        if (startTime == "r")
        {
            return;
        }

        ok = false;

        while (!ok)
        {
            Console.WriteLine();
            endTime = input.GetInput("Enter the End Time in format dd/MM/yy HH:mm:ss.");
            ok = validation.ValidateDate(endTime);
        }

        if (endTime == "r")
        {
            return;
        }

        var mySession = new CodingSession { Id = id, StartTime = startTime, EndTime = endTime };
        var sql = "UPDATE session SET StartTime = @StartTime, EndTime = @EndTime WHERE Id = @Id";
        ExecuteQuery(connectionString, sql, mySession);            
    }

    public void Delete(string connectionString)
    {
        string response = string.Empty;
        int id = 0;
        bool ok = false;
        UserInput input = new UserInput();
        Validation validation = new Validation();

        View(connectionString);

        while (!ok)
        {
            Console.WriteLine();
            response = input.GetInput("Enter the Id to delete.");
            ok = validation.ValidateInt(response);
        }

        if (response == "r")
        {
            return;
        }

        id = Int32.Parse(response);
        var mySession = new CodingSession { Id = id };
        var sql = "DELETE FROM session WHERE Id = @Id";
        ExecuteQuery(connectionString, sql, mySession);      
    }

    public void View(string connectionString)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            var sql = "SELECT * FROM session";
            List<CodingSession> sessions = connection.Query<CodingSession>(sql).ToList();
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("StartTime");
            table.AddColumn("EndTime");
            table.AddColumn("Duration");
            foreach (var session in sessions)
            {
                session.CalculateDuration();
                table.AddRow($@"[blue]{session.Id}[/]", $@"[blue]{session.StartTime}[/]", $@"[blue]{session.EndTime}[/]", $@"[blue]{session.Duration}[/]");
            }

            Console.WriteLine();
            AnsiConsole.Write(table);
        }
    }
}