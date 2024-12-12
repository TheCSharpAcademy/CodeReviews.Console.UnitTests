using CodingLogger.Controller;
using CodingLogger.Data;
using CodingLogger.Models;
using CodingLogger.Services;
using Spectre.Console;
using System.Configuration;

class Application
{
    private static CodingService s_codingService;
    private static UserInput s_userInput;
    private static CodingController s_codingController;
    private static CodingSessionRepository s_codingSessionRepository;
    public static string DBName
    {
        get => ReadSetting("DB_NAME");
    }
    public static string ConnectionString
    {
        get => GetConnectionString();
    }
    private static async Task Main()
    {

        try
        {

            InitializeNeccessaryClasses();
            while (true)
            {
                s_codingController.DisplayMenu();
                string userInput = s_userInput.GetUserInput("Your option?");
                await s_userInput.HandleUserInput(userInput);

            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]Error:[/]{ex.Message}");
        }
    }
    private static void InitializeNeccessaryClasses()
    {
        InitializeDB();
        InitializeHelperServices();


    }
    static string ReadSetting(string key)
    {

        try
        {
            
            var appSettings = ConfigurationManager.AppSettings;
            string? result = appSettings.Get(key);
            if (string.IsNullOrEmpty(result))
            {
                throw new NullReferenceException("The key cannot be found");
            }
            return result;

        }
        catch (ConfigurationErrorsException)
        {
            throw new Exception("Error reading app settings");
        }
    }
    static string GetConnectionString()
    {
        try
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            connectionString = connectionString.
                Replace("{PATH}", GetFilePath(DBName));
            return connectionString;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private static void InitializeDB()
    {
        string tableName = GetDBNameWithoutExtension(DBName);
        var connection = new SqliteConnectionFactory();
        new DBStorage(connection, ConnectionString, tableName);
    }

    private static void InitializeHelperServices()
    {
        s_codingController = new CodingController();
        s_codingSessionRepository = new CodingSessionRepository(ConnectionString);
        s_userInput = new UserInput(null);
        s_codingService = new CodingService(s_codingSessionRepository, s_userInput, s_codingController);
        s_userInput = new UserInput(s_codingService);

    }

    private static string GetDBNameWithoutExtension(string dbName)
    {
        return dbName.Replace(".db", "");
    }
    private static string GetCurrentPath()
    {
        return Environment.CurrentDirectory.Replace("bin/Debug/net7.0", "");
    }

    private static string GetFilePath(string tableName)
    {
        return Path.Combine(GetCurrentPath(), tableName);
    }
}
