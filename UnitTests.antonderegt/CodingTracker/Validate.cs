namespace CodingTracker;

public class Validate
{
    public static bool CanEndSession(CodingSession session)
    {
        return !session.StartTime.Equals(DateTime.MinValue);
    }

    public static bool EndTimeIsAfterStartTime(CodingSession session, DateTime endTime)
    {
        return session.StartTime.CompareTo(endTime) < 0;
    }

    public static int ParseSessionId(string promptedId)
    {
        if (!int.TryParse(promptedId, out int result))
        {
            return -1;
        }

        if (result < 0)
        {
            return -1;
        }

        return result;

    }

    public static bool TryParseDateTime(string promptedTime, out DateTime parsedTime)
    {
        return DateTime.TryParse(promptedTime, out parsedTime);
    }
}