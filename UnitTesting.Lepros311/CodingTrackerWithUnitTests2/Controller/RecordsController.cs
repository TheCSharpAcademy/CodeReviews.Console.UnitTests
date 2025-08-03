using System.Configuration;
using System.Data.SQLite;


namespace CodingTracker
{
    class RecordsController
    {
        public static void AddRecord()
        {
            Display.PrintAllRecords("Add Record");

            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            DateTime? date = UI.PromptForDate();

            DateTime? startTime = UI.PromptForTime("start");

            DateTime? endTime = UI.PromptForTime("end");

            var validation = new Validation();

            while (validation.ValidateStartTimeIsLessThanEndTime(startTime, endTime) == false)
            {
                Console.WriteLine("The end time must be later than the start time.");
                endTime = UI.PromptForTime("end");
            } 

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string insertQuery = @"
                    INSERT INTO CodingSessions (Date, StartTime, EndTime)
                    VALUES (@date, @startTime, @endTime)";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = insertQuery;
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@startTime", startTime);
                    command.Parameters.AddWithValue("@endTime", endTime);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Add Record");
                        Console.WriteLine("\nRecord added successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to add record. Please try again.");
                    }
                }
            }
        }

        public static void EditRecord()
        {
            Display.PrintAllRecords("Edit Record");

            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            int recordId = UI.PromptForRecordId("edit");

            DateTime? date = null;
            DateTime? startTime = null;
            DateTime? endTime = null;

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string selectQuery = @"
                    SELECT Date, StartTime, EndTime
                    FROM CodingSessions
                    WHERE Id = @recordId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = selectQuery;
                    command.Parameters.AddWithValue("@recordId", recordId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            date = reader.GetDateTime(0);
                            startTime = reader.GetDateTime(1);
                            endTime = reader.GetDateTime(2);
                        }
                    }
                }
            }

            Display.PrintEditRecordData(recordId, date, startTime, endTime);

            date = UI.PromptForNewDate(date);
            startTime = UI.PromptForNewTime(startTime, "start");
            endTime = UI.PromptForNewTime(endTime, "end");

            var validation = new Validation();

            while (validation.ValidateStartTimeIsLessThanEndTime(startTime, endTime) == false)
            {
                Console.WriteLine("The end time must be later than the start time.");
                endTime = UI.PromptForTime("end");
            }

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string updateQuery = @"
                    UPDATE CodingSessions
                    SET Date = @date, StartTime = @startTime, EndTime = @endTime
                    WHERE Id = @recordId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = updateQuery;
                    command.Parameters.AddWithValue("@date", date);
                    command.Parameters.AddWithValue("@startTime", startTime);
                    command.Parameters.AddWithValue("@endTime", endTime);
                    command.Parameters.AddWithValue("@recordId", recordId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Edit Record");
                        Console.WriteLine("\nRecord updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed to update Record. Please try again.");
                    }
                }
            }
        }

        public static void DeleteRecord()
        {
            Display.PrintAllRecords("Delete Record");

            string? dbPath = ConfigurationManager.AppSettings.Get("dbPath");

            int recordId = UI.PromptForRecordId("delete");

            if (UI.PromptForDeleteConfirmation(recordId) == "n") { return; }

            using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
            {
                connection.Open();

                string deleteQuery = @"
                    DELETE FROM CodingSessions
                    WHERE Id = @recordId";

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = deleteQuery;
                    command.Parameters.AddWithValue("@recordId", recordId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Display.PrintAllRecords("Delete Record");
                        Console.WriteLine("\nRecord deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("\nNo record found with that ID. Deletion failed.");
                    }
                }
            }
        }
    }
}
