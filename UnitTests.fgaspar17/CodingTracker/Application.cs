namespace CodingTracker;

public class Application
{
    public void Run(string connectionString)
    {
        bool continueRunning = true;
        CrudMenu crudMenu = new CrudMenu();

        while (continueRunning)
        {
            CrudMenuOptions crudMenuOptionPicked = DisplayMenu.ShowMenu<CrudMenuOptions>(crudMenu);
            HandleMenuOptions.HandleCrudMenuOption(crudMenuOptionPicked, connectionString, ref continueRunning);
        }
    }
}
