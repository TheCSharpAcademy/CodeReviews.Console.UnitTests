using Coding_Tracking_Application.Services;

namespace Coding_Tracking_Application;

public class Worker
{
    public static void Execute()
    {
        string inputSelection = UserInput.MainMenuOptions();

        bool parseSuccess = ValidationServices.ParseMenuInput(inputSelection).Item2;
        int parseOutput = ValidationServices.ParseMenuInput(inputSelection).Item1;

        UserInputSelection(parseOutput);

        void UserInputSelection(int userMenuInput)
        {
            UserInput UserInput = new UserInput();
            DatabaseServices DatabaseServices = new();

            switch (userMenuInput)
            {
                case 0: Environment.Exit(0); break;
                case 1: DatabaseServices.GetSessionList(); break;
                case 2: UserInput.CreateEntryInput(); break;
                case 3: DatabaseServices.DeleteEntry(); break;
                case 4: UserInput.AddSessionInput(); break;
                default: Console.Clear(); Console.WriteLine("\n\nSorry, that input is not recognised. Please try again\n\n"); Execute(); break;
            }
        }

        if (parseSuccess && parseOutput >= 0 && parseOutput < 5)
        {
            UserInputSelection(parseOutput);
        }
        else { Execute(); }
    }
}
