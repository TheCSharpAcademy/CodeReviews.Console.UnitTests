using System.Text.RegularExpressions;
using Spectre.Console;

namespace CodingTracker.TwilightSaw
{
    internal class UserInput
    {
        private string input;
        private int inputInt;
        
        public int CreateSpecifiedInt(int bound, string message)
        {
            input = Console.ReadLine();
            while (!Int32.TryParse(input, out inputInt))
            {
                Console.Write(message);
                input = Console.ReadLine();
            }
            while (int.Parse(input) > bound || int.Parse(input) < 1)
            {
                Console.Write(message);
                input = Console.ReadLine();
                inputInt = Int32.Parse(input);
            }
            return inputInt;
        }

        public string CreateRegex(string regexString, string messageStart, string messageError)
        {
            Regex regex = new Regex(regexString);
            var input = AnsiConsole.Prompt(
                new TextPrompt<string>($"[lightgreen]{messageStart}[/]")
                    .Validate(value => regex.IsMatch(value)
                        ? ValidationResult.Success()
                        : ValidationResult.Error(messageError)));
            
           return input;
        }

        public static CodingSession ChooseSession(List<CodingSession> data)
        {
            var chosenSession = AnsiConsole.Prompt(
                new SelectionPrompt<CodingSession>()
                    .Title("[blue]Please, choose an option from the list below:[/]")
                    .PageSize(10)
                    .AddChoices(
                        data));
            return chosenSession;
        }

        public static string CheckT(string dateInput)
        {
            return dateInput is "T" or "t" ? DateTime.Now.ToShortDateString() : dateInput;
        }

        public static string CheckN(string timeInput)
        {
            return timeInput is "N" or "n" ? DateTime.Now.ToLongTimeString() : timeInput;
        }
    }
}
