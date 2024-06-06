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

            bool PassedIsNot0Test = IsNot0(numberInput);
            bool PassedIsValidIntTest = IsValidInt(numberInput);
            bool PassedIsNonNegativeNumber = IsNonNegativeNumber(numberInput);
            bool PassedIsNonStringValue = IsNonStringValue(numberInput);

            if (PassedIsNot0Test == false || PassedIsValidIntTest == false || PassedIsNonNegativeNumber == false || PassedIsNonStringValue)
            {
                Console.WriteLine("Invalid Input. Press Any Key To Go back to MainMenu");
                Console.ReadLine();
                MainMenu.GetUserInput();
            }

            Console.WriteLine("All validation passed. Proceeding to converting to finalInput and start handling given RecordId Entry");
            
            int finalInput = ConvertNumberInputToFinalInput(numberInput);

            return finalInput;
        }

        public static bool IsNot0(string input)
        {
            int parsedNumber;

            bool isInteger = int.TryParse(input, out parsedNumber);

            if (isInteger && parsedNumber != 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        public static bool IsValidInt(string input)
        {
            int parsedNumber; 

            bool isInteger = int.TryParse(input, out parsedNumber);

            if (isInteger)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        public static bool IsNonNegativeNumber(string input)
        {
            int parsedNumber;

            bool isInteger = int.TryParse(input, out parsedNumber);

            if (isInteger && parsedNumber >= 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
        public static bool IsNonStringValue(string input)
        {
            bool isInteger = int.TryParse(input, out int parsedNumber);

            if (isInteger && parsedNumber >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int ConvertNumberInputToFinalInput(string numberInput)
        {
            int finalnumber;

            int.TryParse(numberInput, out finalnumber);

            return finalnumber;
        }        
    }    
}
