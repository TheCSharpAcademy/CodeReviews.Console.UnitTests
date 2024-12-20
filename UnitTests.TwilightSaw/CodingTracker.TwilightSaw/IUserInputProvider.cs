namespace CodingTracker.TwilightSaw;

public interface IUserInputProvider
{
    string ReadInput();

    string ReadRegexInput(string regexString, string messageStart, string messageError);
}