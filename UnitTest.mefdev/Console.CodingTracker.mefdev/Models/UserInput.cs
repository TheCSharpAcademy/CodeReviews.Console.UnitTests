using System;
using System.Globalization;
using CodingLogger.Controller;
using CodingLogger.Services;
using Spectre.Console;


namespace CodingLogger.Models
{
    public class UserInput
    {
        private const string DATE_TIME_FORMAT = "yyyy-MM-dd HH:mm";
        private readonly CodingService _codingService;

        public UserInput(CodingService codingService)
        {
            _codingService = codingService;
        }

        public string GetUserInput(string prompt)
        {
            return AnsiConsole.Ask<string>($"[bold DodgerBlue1]{prompt}[/]");
        }

        public async Task HandleUserInput(string userInput)
        {

            switch (userInput)
            {
                case "a":
                    await _codingService.CreateCodingSession();
                    break;
                case "v":
                    await _codingService.GetCodingSession();
                    break;
                case "d":
                    await _codingService.DeleteCodingSession();
                    break;
                case "u":
                    await _codingService.UpdateCodingSession();
                    break;
                case "s":
                    await _codingService.GetAllCodingSessions();
                    break;
                default:
                    _codingService.QuitCodingSession();
                    break;
            }
        }
        public DateTime GetDateTimeValue(string prompt)
        {
            AnsiConsole.Markup($"[bold DodgerBlue1]{prompt} (format: yyyy-MM-dd HH:mm) [/]");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input cannot be empty. Please provide a valid date and time.");
            }

            if (DateTime.TryParseExact(input, DATE_TIME_FORMAT, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userDateTime))
            {
                return userDateTime;
            }
            else
            {
                throw new FormatException("Invalid date or time format. Please ensure you use the correct format.");
            }
        }
    }
}

