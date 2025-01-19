using Spectre.Console;

namespace CodingTrack
{
    internal class UserInput
    {
        public static DateTime GetTimeInput()
        {
            AnsiConsole.MarkupLine("\n[red]Enter date and time(09/12/2024 00:12:10) or press 0 to exit: [/]\n");
            AnsiConsole.WriteLine();
            string? userDateInput = Console.ReadLine();
            if(userDateInput == "0") {
                Menu.GetUserInput(); 
            }
            DateTime dateTime = default;
            while (!Validators.IsValidDate(userDateInput,  ref dateTime))
            {
                AnsiConsole.MarkupLine("\n[red]Wrong date or time please try again![/]\n");
                userDateInput = Console.ReadLine();
                if (userDateInput == "0")
                {
                    Menu.GetUserInput();
                }

            }
            return dateTime;


        }
        public static void GetAllTimesInput(ref DateTime startTime, ref DateTime endTime, ref TimeSpan duration)
        {
            startTime = GetTimeInput();
            endTime = GetTimeInput();
            while (Validators.IsStartTimeLater(startTime, endTime))
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[red]EndTime cant be less than StartTime, try again![/]");
                endTime = GetTimeInput();
                startTime = GetTimeInput();

            }
            duration = endTime - startTime;
        }
        
    }
}
