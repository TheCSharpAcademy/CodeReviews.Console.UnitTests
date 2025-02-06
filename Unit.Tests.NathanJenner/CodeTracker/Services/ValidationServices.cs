namespace Coding_Tracking_Application.Services;

public class ValidationServices()
{
    public static Tuple<int, bool> ParseMenuInput(string menuInput)
    {
        if (int.TryParse(menuInput, out int parsedInput))
        {
            return Tuple.Create(parsedInput, true);
        }
        else
        {
            return Tuple.Create(-1, false);
        }
    }

    public static Tuple<DateTime, bool> ParseInputDateTime(string inputDateTime)
    {
        if (DateTime.TryParse(inputDateTime, out DateTime parsedDateTime))
        {
            return Tuple.Create(parsedDateTime, true);
        }
        else
        {
            return Tuple.Create(parsedDateTime, false);
        }
    }

    public static bool StartDateLessThanEndDate(DateTime startTime, DateTime endTime)
    {
        return (startTime < endTime);
    }
}
