namespace CodingTracker.TwilightSaw;

public class UserInputProvider : IUserInputProvider
{
    public string ReadInput()
    {
        return Console.ReadLine();
    }
}