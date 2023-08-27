using System.Globalization;

namespace CodingTrackerJMS;

public class Validation
{
    int idInteger = 0;

    public (int id, bool isIdValid) GetValidID(string idString)
    {
        bool isIdValid;

            if (string.IsNullOrEmpty(idString))
            {
                Console.WriteLine("Input is null!");
                isIdValid = false;
            }
            else
            {
                if (int.TryParse(idString, out idInteger))
                {
                    isIdValid = true;
                }
                else
                {
                    isIdValid = false;
                    Console.WriteLine("Invalid input! The ID must be a valid integer.");
                }
            }
        

        int id = idInteger;

        return (id, isIdValid);
    }

    public bool GetValidDate(string inputDate, bool isValid, out DateTime startDateT)
    {
        string format = "yyyy/MM/dd; HH:mm";

            if (DateTime.TryParseExact(inputDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDateT))
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter a date in the correct format (yyyy/mm/dd.");
            }

        return isValid;
    }
}
