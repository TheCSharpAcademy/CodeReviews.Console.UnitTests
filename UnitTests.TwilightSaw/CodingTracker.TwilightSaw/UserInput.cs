using System.Text.RegularExpressions;
using Spectre.Console;

namespace CodingTracker.TwilightSaw
{
    public class UserInput(IUserInputProvider inputProvider)
    {
        public int CreateSpecifiedInt(int bound, string message)
        {
            var inputInt = 0;
            var input = inputProvider.ReadInput();
            while (int.TryParse(input, out inputInt))
            {
                Console.Write(message);
                input = inputProvider.ReadInput();
            }
            while (!int.TryParse(input, out inputInt) || inputInt < 1 || inputInt > bound)
            {
                Console.Write(message);
                input = inputProvider.ReadInput();
                int.TryParse(input, out inputInt);
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
