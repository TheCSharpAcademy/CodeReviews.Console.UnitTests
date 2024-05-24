using System.Globalization;

namespace CodeTrackerConfigFirst
{
    public class Validation
    {

        public static int GetNumberInput(string message)
        {
            Console.WriteLine(message);

            string numberInput = Console.ReadLine();

            if (numberInput == "0")
            {
                MainMenu.GetUserInput();
            }

            try
            {
                while (!int.TryParse(numberInput, out _) || Convert.ToInt32(numberInput) < 0)
                {
                    Console.WriteLine("\n\nInvalid number. Try again.\n\n");
                    numberInput = Console.ReadLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\n\nInvalid input format. Please enter a valid number.\n\n");
                throw; // Re-throw the caught exception to propagate it further
            }

            int finalInput = Convert.ToInt32(numberInput);

            return finalInput;
        }   
    }
}
