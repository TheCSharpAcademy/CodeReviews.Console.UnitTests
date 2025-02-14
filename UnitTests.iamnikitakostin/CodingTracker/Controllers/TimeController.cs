namespace CodingTracker.Controllers
{
    public class TimeController
    {
        static public string ConvertFromSeconds(int seconds)
        {
            if (seconds < 0) {
                return "error";
            }

            TimeSpan time = TimeSpan.FromSeconds(seconds);
            string answer = time.ToString(@"hh\:mm\:ss");
            return answer;
        }
    }
}
