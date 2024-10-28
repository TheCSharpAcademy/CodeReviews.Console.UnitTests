using Spectre.Console;

namespace CodingTracker;

public interface IValidator<T>
{
    ValidationResult Validate(T? input);
}
