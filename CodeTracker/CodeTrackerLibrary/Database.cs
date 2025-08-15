using System.Xml.Linq;
using Dapper;
using Microsoft.Data.Sqlite;

namespace CodeTrackerLibrary;

public static class Database
{
    private static string connectionString  = string.Empty;

    public static void InitializeDatabase()
    {
     
        XDocument config = XDocument.Load("config.xml");
       
        connectionString = config.Element("configuration").Element("database").Element("connectionString").Value;
        //Initialisation logic
        using (var connection = new SqliteConnection(connectionString))
        {
            
            connection.Open();
            string createTable = @"CREATE TABLE IF NOT EXISTS CodingSessions (
        Id INTEGER PRIMARY KEY AUTOINCREMENT, 
        Date TEXT NOT NULL ,
        StartTime TEXT NOT NULL,
        EndTime TEXT NOT NULL,
        Duration TEXT NOT NULL ,
        Description TEXT NOT NULL)";
            connection.Execute(createTable);
        }
    }

    public static void AddCodingSession(CodingSession codingSession)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string command = @"INSERT INTO CodingSessions(Date,StartTime,EndTime,Duration,Description) 
            VALUES (@Date, @StartTime, @EndTime, @Duration,@Description)";
        connection.Execute(command, new
        {
            Date = codingSession.StartTime.ToString(CodingSession.DayFormat),
            StartTime = codingSession.StartTime.ToString(CodingSession.TimeFormat),
            EndTime = codingSession.EndTime.ToString(CodingSession.TimeFormat),
            Duration = codingSession.DurationToString(),
            Description = codingSession.Description
        });
        connection.Close();
    }

    public static List<CodingSession> GetCodingSessionRecord(CodingSession sessionDetails)
    {
        var codingSessions = new List<CodingSession>();
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string command = @"SELECT Date, StartTime, EndTime, Description FROM CodingSessions ";
        if (sessionDetails.StartTime != DateTime.MinValue)
            command += @"WHERE Date = @Date ";
        if (!String.IsNullOrWhiteSpace(sessionDetails.Description) )
            command += (sessionDetails.StartTime != DateTime.MinValue)? (@"AND Description LIKE @Description "):(@"WHERE Description LIKE @Description ");

        var parameters = new {Date = sessionDetails.StartTime.ToString(CodingSession.DayFormat), Description = $"%{sessionDetails.Description}%"};
        try
        {
            codingSessions = connection.Query<CodingSession>(command, parameters).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.ReadLine();
        }
        return codingSessions;
        
    }

    public static void DeleteCodingSession(CodingSession codingSession)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string command = @"DELETE FROM CodingSessions WHERE Date = @Date AND Description LIKE  @Description";
        int deletions = connection.Execute(command,new {Date = codingSession.StartTime.ToString(CodingSession.DayFormat), Description =
            $"%{codingSession.Description}%"});
        Console.WriteLine($"Deleted {deletions} records");
        Console.ReadLine();
    }

    public static void UpdateCodingSession(CodingSession oldSession , CodingSession newSession)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        string command = @"UPDATE CodingSessions SET Date = @Date , Description = @Description WHERE Date = @oldDate AND Description LIKE @oldDescription";
        var parameters = new {Date = newSession.StartTime.ToString(CodingSession.DayFormat), Description = newSession.Description, oldDate = oldSession.StartTime.ToString(CodingSession.DayFormat), oldDescription = $"%{oldSession.Description}%" };
        int updatedRecodsAmount = connection.Execute(command, parameters);
        Console.WriteLine($"Updated {updatedRecodsAmount} records");
        if ( updatedRecodsAmount == 0 )
            Console.WriteLine($"No Records Updated mean No entry was found with Details : {oldSession}");
        Console.ReadLine();
        
    }

}