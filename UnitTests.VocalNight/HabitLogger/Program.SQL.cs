using Microsoft.Data.Sqlite;

partial class Program
{
    const string connectionAdress = @"Data Source=habits.db";
    static void DeleteAHabit()
    {

        Console.WriteLine("Type the name of the habit you want to delete");
        string habit = Console.ReadLine();

        using (var connection = new SqliteConnection("Data Source=habits.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                @$"DROP TABLE {habit}";

            command.ExecuteNonQuery();
            Console.WriteLine("Command Executed");
            connection.Close();
        }
    }

    static void DeleteHabitEntry()
    {

        Console.WriteLine("What habit you want to edit?");
        string habit = Console.ReadLine();

        Console.WriteLine("Whats the id of the entry you want to delete?");
        string id = Console.ReadLine();

        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                @$"
            DELETE from {habit} WHERE id = {id}
        ";
            command.ExecuteNonQuery();
            Console.WriteLine("Command Executed");
            connection.Close();
        }
    }

    static void InsertHabitEntry()
    {

        Console.WriteLine("What habit you want to edit?");
        string habit = Console.ReadLine();

        Console.WriteLine("Insert the number of times");
        string numberOfTimes = Console.ReadLine();

        string date = GetDateInput();


        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                $"INSERT INTO {habit} (TimesDone, Date) VALUES ({numberOfTimes}, '{date}')";

            command.ExecuteNonQuery();
            Console.WriteLine("Command Executed");
            connection.Close();
        }
    }

    static void ViewHabitEntries()
    {

        Console.WriteLine("Type the name of the habit you want to check");
        string habit = Console.ReadLine();


        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                $"SELECT * FROM {habit}";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var date = reader.GetString(1);
                    var timesDone = reader.GetString(2);
                    Console.WriteLine("----------------");

                    Console.WriteLine(
                        @$"Id: {reader.GetString(0)}
Times done: {timesDone}
Date done: {date}");

                }
            }
            connection.Close();
        }
    }

    static void InsertNewHabit()
    {
        Console.WriteLine("Type the name of the habit you want to create");
        string habit = Console.ReadLine();

        using (var connection = new SqliteConnection(connectionAdress))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText =
                @$"CREATE TABLE IF NOT EXISTS {habit} (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Date TEXT,
            TimesDone INTEGER
            )";

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    static string GenerateUpdateSQL( string response, string habit, string id, string numberOfTimes )
    {
        if (response == "y")
        {

            string date = GetDateInput();

            return @$"
            UPDATE {habit}
            SET timesDone = {numberOfTimes},
            lastTime = '{date}'
            WHERE id = {id}";

        }
        else
        {
            return @$"
            UPDATE {habit}
            SET timesDone = {numberOfTimes},
            WHERE id = {id}";
        }
    }
}

