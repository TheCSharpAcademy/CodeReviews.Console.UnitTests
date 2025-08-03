using CodingTracker.Model;
using System.Configuration;

namespace CodingTracker
{
    class UI
    {
        public static DateTime? PromptForDate()
        {
            DateTime? date = null;
            do
            {
                Console.Write("\nEnter the date (mm/dd/yyyy): ");
                string? dateInput = Console.ReadLine();
                var validation = new Validation();
                date = validation.ValidateDate(dateInput!);
                if (date == null)
                {
                    Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                }
            } while (date == null);

            return date;
        }

        public static DateTime? PromptForNewDate(DateTime? date)
        {
            do
            {
                Console.Write("\nEnter new date (mm/dd/yyyy) (leave blank to keep current): ");
                string? dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    return date;
                }
                else
                {
                    var validation = new Validation();
                    date = validation.ValidateDate(dateInput);
                    if (date == null)
                    {
                        Console.WriteLine("Invalid date format. Please enter a date in the format mm/dd/yyyy.");
                    }
                    else
                    {
                        var validation2 = new Validation();
                        date = validation.ValidateDate(dateInput);
                    }
                }
            } while (date == null);

            return date;
        }

        public static DateTime? PromptForTime(string promptText)
        {
            DateTime? time;
            do
            {
                Console.Write($"\nEnter the {promptText} time (hh:mm am/pm): ");
                string timeInput = Console.ReadLine();
                var validation = new Validation();
                time = validation.ValidateTime(timeInput);
                if (time == null)
                {
                    Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm am/pm.");
                }
            } while (time == null);

            return time;
        }

        public static DateTime? PromptForNewTime(DateTime? time, string promptText)
        {
            do
            {
                Console.Write($"\nEnter new {promptText} time (hh:mm am/pm) (leave blank to keep current): ");
                string? timeInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(timeInput))
                {
                    break;
                }
                else
                {
                    var validation = new Validation();
                    time = validation.ValidateTime(timeInput);
                    if (time == null)
                    {
                        Console.WriteLine("Invalid time format. Please enter a time in the format hh:mm am/pm.");
                    }
                }
            } while (time == null);

            return time;
        }

        public static int PromptForRecordId(string promptText)
        {
            int recordId = 0;
            bool isValidInput = false;

            var connection = ConfigurationManager.ConnectionStrings["connection"]?.ConnectionString;
            ICodingSessionRepository repo = new CodingSessionRepository(connection);
            var validation = new Validation(repo);

            do
            {
                Console.Write($"\nEnter the ID of the record you want to {promptText}: ");
                string? recordIdInput = Console.ReadLine();

                (string message, bool validStatus, int recordId) result = validation.ValidateRecordId(recordIdInput);
                Console.WriteLine($"{result.message}");
                isValidInput = result.validStatus;
                recordId = result.recordId;
            } while (isValidInput == false);

            return recordId;
        }

        public static string PromptForDeleteConfirmation(int recordId)
        {
            string? confirmation;
            bool isValidInput = false;
            do
            {
                Console.Write($"Are you sure you want to delete the record with ID {recordId}? (y/n): ");
                confirmation = Console.ReadLine();
                var validation = new Validation();
                string validatedConfirmation = validation.ValidateDeleteConfirmation(confirmation);
                if (validatedConfirmation == "n")
                {
                    Console.WriteLine("Deletion canceled.");
                    return "n";
                }
                else if (validatedConfirmation != "y")
                {
                    Console.WriteLine("Invalid response.\n");
                }
                else
                {
                    isValidInput = true;
                }
            } while (isValidInput == false);

            return "y";
        }

        public static void ReturnToMainMenu()
        {
            Console.Write("\nPress any key to return to the Main Menu...");
            Console.ReadKey();
        }
    }
}
