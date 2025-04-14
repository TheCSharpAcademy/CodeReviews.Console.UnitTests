using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker;

public class SpectreValidation(IConfiguration config)
{
    private readonly string _dtFormat = config["DateTimeFormat"] ?? "yyyy-MM-dd HH:mm";

    private string Red(string message)
    {
        return "[red bold]" + message + "[/]";
    }

    public ValidationResult Time(string dateTime)
    {
        if (!DateTime.TryParseExact(
            dateTime,
            _dtFormat,
            new CultureInfo("en-US"),
            DateTimeStyles.None,
            out DateTime _))
        {
            return ValidationResult.Error(Red("Invalid date/time."));
        }

        return ValidationResult.Success();
    }

    public ValidationResult EndTime(string start, string end)
    {
        if (String.Compare(start, end) > 0)
        {
            return ValidationResult.Error(Red("End time cannot be before start time."));
        }

        return ValidationResult.Success();
    }

    public ValidationResult Year(string yearStr)
    {
        if (yearStr.Length != 4)
        {
            return ValidationResult.Error(Red("Invalid format."));
        }

        if (int.TryParse(yearStr, out int yearInt))
        {
            if (yearInt > DateTime.Now.Year)
                return ValidationResult.Error(Red("Year cannot be in the future."));
            return ValidationResult.Success();
        }
        else
        {
            return ValidationResult.Error(Red("Invalid year format."));
        }
    }

    public ValidationResult Month(string monthStr)
    {
        bool valid = DateTime.TryParseExact(
            monthStr,
            "yyyy-MM",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime monthDT);

        if (!valid)
        {
            return ValidationResult.Error(Red("Invalid format."));
        }

        if (monthDT > DateTime.Now)
        {
            return ValidationResult.Error(Red("Month cannot be in the future."));
        }

        return ValidationResult.Success();
    }

    public ValidationResult MinDuration(int minutes)
    {
        if (minutes < 0)
        {
            return ValidationResult.Error(Red("Must be a positive integer."));
        }

        return ValidationResult.Success();
    }

    public ValidationResult MaxDuration(int minutes, int minimum)
    {
        if (minutes < minimum)
        {
            return ValidationResult.Error(Red($"Cannot be less than the minimum value of {minimum} minutes."));
        }

        return ValidationResult.Success();
    }

    public ValidationResult PositiveId(int id)
    {
        if (id < 0)
        {
            return ValidationResult.Error(Red("Must be a positive integer."));
        }

        return ValidationResult.Success();
    }
}
