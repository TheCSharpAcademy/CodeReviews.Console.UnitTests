using ConConfig;
using Spectre.Console;

namespace CodingTracker.Cactus
{
    public static class CodeSessionHelper
    {
        public static CodingSession InputExercise()
        {
            Console.WriteLine("Please type start date");
            DateTime startDate = GetValidStartDate();

            Console.WriteLine("Please type end date");
            DateTime endDate = GetValidEndDate(startDate);

            CodingSession codeSession = new CodingSession(startDate, endDate);

            return codeSession;
        }


        public static DateTime GetValidStartDate()
        {
            var dateStr = AnsiConsole.Ask<string>("Date (format: HH:mm dd-MM-yyyy):");
            DateTime date;
            while (!Validator.IsValidDate(dateStr, out date))
            {
                Console.WriteLine("Please type correct format date.");
                dateStr = AnsiConsole.Ask<string>("Date (format: HH:mm dd-MM-yyyy):");
            }

            return date;
        }


        public static DateTime GetValidEndDate(DateTime startDate)
        {
            DateTime endDate = GetValidStartDate();
            while (endDate < startDate)
            {
                Console.WriteLine($"End date should late than start date {startDate}.");
                var endDateStr = AnsiConsole.Ask<string>("End date (format: HH:mm dd-MM-yyyy): ");
                while (!Validator.IsValidDate(endDateStr, out endDate))
                {
                    Console.WriteLine("Please type correct format date.");
                    endDateStr = AnsiConsole.Ask<string>("End date (format: HH:mm dd-MM-yyyy): ");
                }
            }
            return endDate;
        }


        public static int GetValidInputId(HashSet<int> ids)
        {
            Console.WriteLine($"Please type a id you want to operate:");
            int id = -1;
            string? idStr = Console.ReadLine();
            while (!int.TryParse(idStr, out id) || !ids.Contains(id))
            {
                Console.WriteLine($"Sorry, your id is invalid. Please type a valid id:");
                idStr = Console.ReadLine();
            }
            return id;
        }
    }
}
