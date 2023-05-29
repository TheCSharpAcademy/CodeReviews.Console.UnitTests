using System.Globalization;
using Microsoft.Data.Sqlite;
using ConsoleTableExt;

namespace CodeTracker;

public class UserInput
{
    internal static string connectionsString = @"Data Source=habit-Tracker.db";
    static bool totalTime;
    static bool userRecord;
    internal static void GetUserInput()
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to View All Records.");
            Console.WriteLine("Type 2 to New Record with System Time.");
            Console.WriteLine("Type 3 to New Record with User Entering Time.");
            Console.WriteLine("Type 4 to Delete a Record.");
            Console.WriteLine("Type 5 to Total Time Coded.");
            Console.WriteLine("------------------------------------------\n");

            string command = Console.ReadLine();

            switch (command)
            {
                case "0":
                    Console.WriteLine("\nGoodbye!\n");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    RecordTime();
                    break;
                case "3":
                    userRecord = true;
                    RecordTime();
                    break;
                case "4":
                    Delete();
                    break;
                case "5":
                    TotalTimeCoded();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 5.\n");
                    break;
            }
        }
    }

    static void RecordTime()
    {
        string startTime;
        string endTime;

        if (!userRecord)
        {
            startTime = CodingSession.StartCodingSession();
            endTime = CodingSession.EndCodingSession();
        }
        else
        {
            while (true)
            {
                Console.WriteLine("Enter Start Time");
                bool isValidStart = UserEnterTime(out DateTime startDateTime);
                Console.WriteLine("Enter End Time");
                bool isValidEnd = UserEnterTime(out DateTime endDateTime);

                if (!isValidStart || !isValidEnd)
                {
                    Console.WriteLine("Invalid date format. Please try again.");
                    continue;
                }

                startTime = startDateTime.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
                endTime = endDateTime.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                if (startDateTime > endDateTime)
                {
                    Console.WriteLine("Invalid date. Start time has to be before end time.");
                }
                else
                {
                    break;
                }
            }
        }

        using (var connection = new SqliteConnection(connectionsString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"INSERT INTO TestLast(StartTime, EndTime) VALUES('{startTime}', '{endTime}')";

            tableCmd.ExecuteNonQuery();
            connection.Close();
        }
        userRecord = false;
    }

    public static bool UserEnterTime(out DateTime dateTime)
    {
        Console.WriteLine("Please insert the date: (Format: M/d/yyyy h:mm:ss tt). tt is AM or PM. Type 0 to return to the main menu.");
        string dateInput = Console.ReadLine();

        if (dateInput == "0")
        {
            userRecord = false;
            GetUserInput();
            dateTime = default;
            return false;
        }

        if (!DateTime.TryParseExact(dateInput, "M/d/yyyy h:mm:ss tt", new CultureInfo("en-US"), DateTimeStyles.None, out dateTime))
        {
            Console.WriteLine("\n\nInvalid date. (Format: M/d/yyyy h:mm:ss tt).");
            dateTime = default;
            return false;
        }

        return true;
    }

    static void Delete()
    {
        GetAllRecords();
        var recordId = GetNumberInput("\n\nPlease type the Id of the record you want to delete or type 0 to go back to Main Menu\n\n");

        using (var connection = new SqliteConnection(connectionsString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = $"DELETE from TestLast WHERE Id = '{recordId}'";
            int rowCount = tableCmd.ExecuteNonQuery();

            if (rowCount == 0)
            {
                Console.WriteLine($"\n\nRecord with Id {recordId} doesn't exist. \n\n");
                Delete();
            }
        }
        Console.WriteLine($"\n\nRecord with Id {recordId} was deleted. \n\n");
    }

    static void TotalTimeCoded()
    {
        totalTime = true;
        GetAllRecords();
    }

    static void GetAllRecords()
    {
        Console.Clear();
        using (var connection = new SqliteConnection(UserInput.connectionsString))
        {
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $"SELECT * FROM TestLast ";

            List<CodingSession.CodeingTime> tableData = new();
            SqliteDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    tableData.Add(
                    new CodingSession.CodeingTime
                    {
                        Id = reader.GetInt32(0),
                        StartTime = DateTime.ParseExact(reader.GetString(1), "M/d/yyyy h:mm:ss tt", new CultureInfo("en-US")),
                        EndTime = DateTime.ParseExact(reader.GetString(2), "M/d/yyyy h:mm:ss tt", new CultureInfo("en-US")),
                        TimeCoded = CalculateTimeCoded(reader.GetString(1), reader.GetString(2))
                    });
                }

                ConsoleTableBuilder
                    .From(tableData)
                    .WithFormat(ConsoleTableBuilderFormat.Alternative)
                    .ExportAndWriteLine(TableAligntment.Center);
            }
            else
            {
                Console.WriteLine(" No rows found");
            }

            connection.Close();

            if (totalTime)
            {
                var totalQuantity = TimeSpan.FromMinutes(tableData.Sum(dw => dw.TimeCoded.TotalMinutes));
                var formattedTotal = $"{(int)totalQuantity.TotalHours}:{totalQuantity.Minutes:D2}";
                Console.WriteLine($"Total amount of time coded: {formattedTotal}");
            }
        }
        totalTime = false;
    }

    static TimeSpan CalculateTimeCoded(string startTime, string endTime)
    {
        DateTime start = DateTime.ParseExact(startTime, "M/d/yyyy h:mm:ss tt", new CultureInfo("en-US"));
        DateTime end = DateTime.ParseExact(endTime, "M/d/yyyy h:mm:ss tt", new CultureInfo("en-US"));

        return end - start;
    }

    static int GetNumberInput(string message)
    {
        Console.WriteLine(message);
        string numberInput = Console.ReadLine();

        if (numberInput == "0") GetUserInput();

        while (!Int32.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine("\n\nInvalid number. Try again.\n\n");
            numberInput = Console.ReadLine();
        }

        int finalInput = Convert.ToInt32(numberInput);

        return finalInput;
    }
}
