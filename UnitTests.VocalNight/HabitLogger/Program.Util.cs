using HabitLogger;
using Microsoft.Data.Sqlite;

public partial class Program
{
    static void ListHabits()
    {
        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                @"
            SELECT name FROM sqlite_master WHERE type='table' AND name != 'sqlite_sequence'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("----------------");

                    Console.WriteLine(
                        @$"Name: {reader.GetString(0)}");
                }
            }
            connection.Close();
        }
    }

    

    public static string GetDateInput()
    {
        Console.WriteLine("\nPlease insert the date in the format dd-mm-yyyy. Type 0 to return to the main menu");

        string dateInput = Console.ReadLine();

        while (Validation.ValidateDate(dateInput))
        {
            Console.WriteLine("\nInvalid date. Try again:\n");
            dateInput = Console.ReadLine();
        }

        return dateInput;
    }

    static void UpdateHabitEntry()
    {

        Console.WriteLine("What habit you want to edit?");
        string habit = Console.ReadLine();

        Console.WriteLine("Whats the id of the entry you want to edit?");
        string id = Console.ReadLine();

        Console.WriteLine("Insert the number of times");
        string numberOfTimes = Console.ReadLine();

        Console.WriteLine("Do you want to edit the date? y/n");
        string sql = GenerateUpdateSQL(Console.ReadLine(), habit, id, numberOfTimes);

        using (var connection = new SqliteConnection("Data Source=habits.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                sql;

            command.ExecuteNonQuery();
            Console.WriteLine("Command Executed");
            connection.Close();
        }
    }

    static void GetMainInput()
    {

        Console.Clear();
        bool isRunning = true;

        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                @"CREATE TABLE IF NOT EXISTS drink_water (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            TimesDone INTEGER
            )";

            command.ExecuteNonQuery();

            connection.Close();
        }


        while (isRunning)
        {

            Console.WriteLine($@"What would you like to do:
    1 - Insert a new habit
    2 - Insert a new ocurrence of a habit
    3 - Update an existing habit
    4 - Delete an entry in a habit
    5 - Delete a habit
    6 - View entries in a habit
    7 - View all habits
    0 - Exit the program");

            string op = Console.ReadLine();

            switch (op)
            {
                case "1":
                    try
                    {
                        InsertNewHabit();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "2":
                    try
                    {
                        InsertHabitEntry();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "3":
                    try
                    {
                        UpdateHabitEntry();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "4":
                    try
                    {
                        DeleteHabitEntry();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "5":
                    try
                    {
                        DeleteAHabit();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "6":
                    try
                    {
                        ViewHabitEntries();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "7":
                    try
                    {
                        ListHabits();
                    }
                    catch (Exception ex)
                    {
                        DealWithError(ex);
                    }
                    break;
                case "0":
                    Console.WriteLine("\nGoodbye");
                    isRunning = false;
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            Console.WriteLine("-------------------------------------------\n");
        }
    }

    static void DealWithError( Exception ex )
    {
        Console.WriteLine("Something Went wrong! Check what you typed, you might have typed the name of the habit incorrectly!");
    }
}

