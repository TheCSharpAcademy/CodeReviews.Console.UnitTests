using static PhoneBook.Enums;
using Spectre.Console;
namespace PhoneBook;

internal static class UserInterface
{
    internal static bool MainMenu()
    {
        bool isAppRunning = true;
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<Menu>()
            .Title("What would you like to do?")
            .AddChoices(
                Menu.ViewContacts,
                Menu.InsertContact,
                Menu.DeleteContact,
                Menu.UpdateContact,
                Menu.SendEmail,
                Menu.SendSMS,
                Menu.Quit
            )
        );

        switch (option)
        {
            case Menu.ViewContacts:
                Service.ViewContacts();
                break;
            case Menu.InsertContact:
                Service.InsertContact();
                break;
            case Menu.DeleteContact:
                Service.DeleteContact();
                break;
            case Menu.UpdateContact:
                Service.UpdateContact();
                break;
            case Menu.SendEmail:
                Service.SendEmail();
                break;
            case Menu.SendSMS:
                Service.SendSMS();
                break;
            case Menu.Quit:
                isAppRunning = false;
                break;
        }
        return isAppRunning;
    }

    internal static void ShowContacts(List<Contact> contacts, int num)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");
        table.AddColumn("Category");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Id.ToString(), contact.Name, contact.PhoneNumber, contact.Email, contact.Category);
        }
        AnsiConsole.Write(table);
        if (num == 0)
        {
            Console.WriteLine("Press enter to go to main menu.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}