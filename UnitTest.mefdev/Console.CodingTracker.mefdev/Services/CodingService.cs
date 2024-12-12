using CodingLogger.Controller;
using CodingLogger.Data;
using CodingLogger.Models;
using CodingLogger.Shared;
using Spectre.Console;

namespace CodingLogger.Services
{
    public class CodingService : IMaintanable<CodingSession>
    {
        public static ICodingSessionRepository _repository;
        public static UserInput _userInput;
        public static CodingController _codingController;
        public CodingService(ICodingSessionRepository repository, UserInput userInput, CodingController codingController)
        {
            _repository = repository;
            _userInput = userInput;
            _codingController = codingController;

        }
        public async Task CreateCodingSession()
        {
            try
            {
                _codingController.RenderCustomLine("blue", "Add A New Coding Session");
                int uniqueID = GenerateRandomID();
                DateTime start = _userInput.GetDateTimeValue("Enter the start date and time");
                DateTime end = _userInput.GetDateTimeValue("Enter the end date and time");
                long duration = CalculateDuration(start, end);
                var codingSession = new CodingSession(uniqueID, duration, start, end);

                await Add(codingSession);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
            }

        }
        public async Task UpdateCodingSession()
        {
            try
            {
                _codingController.RenderCustomLine("blue", "Update Coding Session");
                string codingSessionID = _userInput.GetUserInput("Enter an id to update a coding session");
                if (!int.TryParse(codingSessionID, out int id))
                {
                    AnsiConsole.MarkupLine($"[red]An error occurred:[/] Invalid ID format. Please enter a valid integer.");
                    return;
                }
                var existingSession = await _repository.Retrieve(id);
                if (existingSession == null)
                {
                    AnsiConsole.MarkupLine("[red]Error: Coding session not found.[/]");
                    return;
                }
                AnsiConsole.MarkupLine($"Current details: [yellow]ID:[/] {existingSession.Id}, [yellow]Duration:[/] {existingSession.GetFormattedDuration()},  [yellow]Start:[/] {existingSession.StartTime}, [yellow]End:[/] {existingSession.EndTime}");
                DateTime start = _userInput.GetDateTimeValue($"Enter new start date and time (current: {existingSession.StartTime:yyyy-MM-dd HH:mm}):");
                DateTime end = _userInput.GetDateTimeValue($"Enter new end date and time (current: {existingSession.EndTime:yyyy-MM-dd HH:mm}):");
                long duration = CalculateDuration(start, end);
                var updatedSession = new CodingSession(existingSession.Id, duration, start, end);

                await Update(updatedSession);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An unexpected error occurred: {ex.Message}[/]");
            }
        }
        public async Task GetCodingSession()
        {
            try
            {
                _codingController.RenderCustomLine("blue", "View A Coding Session");
                string codingSessionID = _userInput.GetUserInput("Enter the coding id to retreive");
                if (!int.TryParse(codingSessionID, out int id))
                {
                    AnsiConsole.MarkupLine($"[red]An error occurred:[/] Invalid ID format. Please enter a valid integer.");
                    return;
                }
                var codingSession = await Get(id);
                if (codingSession == null)
                {
                    AnsiConsole.MarkupLine("[yellow]Notice:[/] No coding session found with the provided ID.");
                    return;
                }
                _codingController.DisplayCodingSession(codingSession);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred:{ex.Message}[/]");
            }

        }
        public async Task GetAllCodingSessions()
        {

            try
            {
                _codingController.RenderCustomLine("blue", "View All Coding sessions");
                var codingSessions = await GetAll();
                _codingController.DisplayCodingSessions(codingSessions);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred:{ex.Message}[/]");
            }

        }
        public async Task DeleteCodingSession()
        {
            try
            {
                _codingController.RenderCustomLine("blue", "Delete A Coding Session");
                string input = _userInput.GetUserInput("Enter the coding session ID to delete");


                if (!int.TryParse(input, out int id))
                {
                    AnsiConsole.MarkupLine("[red]An error occurred:[/] Invalid ID format. Please enter a valid integer.");
                    return;
                }
                await Delete(id);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]An error occurred:[/] {ex.Message}");
            }
        }
        public void QuitCodingSession()
        {
            _codingController.RenderCustomLine("blue", "Quit A Coding Session");
            string? quit = _userInput.GetUserInput("Quit Y/N").ToLower();
            if (quit.Equals("y"))
            {
                AnsiConsole.MarkupLine("[yellow]Quitting the application. Thank you for using our Coding Session APP![/]");
                Environment.Exit(0);
            }
        }


        public async Task Add(CodingSession session)
        {
            var existingCodingSession = await _repository.Retrieve(session.Id);
            if (existingCodingSession != null)
            {
                AnsiConsole.MarkupLine("[red]Error:[/] The coding session already exists.");
                return;
            }
            await _repository.Create(session);
            AnsiConsole.MarkupLine("[green]Success:[/] Item with ID {0} has been created.", session.Id);
        }
        public async Task<CodingSession> Get(int id)
        {
            var codingSession = await _repository.Retrieve(id);
            if (codingSession == null)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] Coding session with ID {0} was not found.", id);
                return null;
            }
            AnsiConsole.MarkupLine("[green]Success:[/] Item with ID {0} has been retrieved.", id);
            return codingSession;

        }
        public async Task Delete(int id)
        {
            var codingSession = await _repository.Retrieve(id);
            if (codingSession == null)
            {
                AnsiConsole.MarkupLine("[yellow]Warning:[/] Item with ID {0} does not exist.", id);
                return;
            }
            AnsiConsole.MarkupLine("[green]Success:[/] Item with ID {0} has been deleted.", id);
            await _repository.Delete(id);
        }
        public async Task<List<CodingSession>> GetAll()
        {
            var codingSessions = await _repository.RetrieveAll();
            if (codingSessions == null || codingSessions.Count == 0)
            {
                AnsiConsole.MarkupLine("[yellow]Notice:[/] No coding sessions found.");
                return new List<CodingSession>();
            }
            return codingSessions;
        }
        public async Task Update(CodingSession newSession)
        {
            if (newSession == null)
            {
                AnsiConsole.MarkupLine("[yellow]Warning:[/] CodingSession cannot be null.");
                return;
            }
            var existingSession = await _repository.Retrieve(newSession.Id);
            if (existingSession == null)
            {
                AnsiConsole.MarkupLine("[yellow]Warning:[/] CodingSession with ID {0} not found.", newSession.Id);
            }
            await _repository.Update(newSession);
            AnsiConsole.MarkupLine("[green]Coding session updated successfully.[/]");

        }

        public long CalculateDuration(DateTime startTime, DateTime endTime)
        {
            if (endTime < startTime)
            {
                throw new ArgumentException("Invalid operation: End time cannot be earlier than start time.");
            }
            return (endTime - startTime).Duration().Ticks;
        }
        public int GenerateRandomID()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());

        }
    }
}
