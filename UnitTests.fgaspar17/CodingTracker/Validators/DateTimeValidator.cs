using Spectre.Console;
using System.Globalization;

namespace CodingTracker;

public class DateTimeValidator : IValidator<string>
{
    private const string DateFormat = "yyyy-MM-dd HH:mm";
    public ValidationResult Validate(string? input)
    {
        if (!DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        {
            return ValidationResult.Error("[red]The Date format must be (yyyy-MM-dd HH:mm).[/]");
        }
        return ValidationResult.Success();
    }

    public ValidationResult FutureValidate(string input, DateTime start)
    {
        if (DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            if (parsedDate <= start)
            {
                return ValidationResult.Error("[red]The End time must be later than the Start time.[/]");
            }
            return ValidationResult.Success();
        }
        return ValidationResult.Error("[red]Invalid date format. Please use (yyyy-MM-dd HH:mm).[/]");
    }
}
