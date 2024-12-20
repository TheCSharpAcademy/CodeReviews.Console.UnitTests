using System.Text.RegularExpressions;
using Spectre.Console;

namespace CodingTracker.TwilightSaw;

public class UserInputProvider : IUserInputProvider
{
    public string ReadInput()
    {
        return Console.ReadLine();
    }

    public string ReadRegexInput(string regexString, string messageStart, string messageError)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[lightgreen]{messageStart}[/]")
                .Validate(value => new Regex(regexString).IsMatch(value)
                    ? ValidationResult.Success()
                    : ValidationResult.Error(messageError)));
    }
}