using System.Net;
using System.Net.Mail;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using static PhoneBook.Enums;

namespace PhoneBook;

internal static class Service
{
    internal static void ViewContacts()
    {
        var option = AnsiConsole.Prompt(new SelectionPrompt<Contacts>()
        .Title("Choose: ")
        .AddChoices(
            Contacts.ViewAllContacts,
            Contacts.FilterByCategory
        ));
        if (option == Contacts.ViewAllContacts)
        {
            var contacts = Controller.Read();
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
                UserInterface.MainMenu();
            }
            UserInterface.ShowContacts(contacts, 0);
        }
        else if (option == Contacts.FilterByCategory)
        {
            var contacts = Controller.Read();
            var categories = contacts.Select(c => c.Category).ToArray();
            var category = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose category: ")
            .AddChoices(categories));
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts available.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
                UserInterface.MainMenu();
            }
            var filteredContacts = contacts.Where(p => p.Category == category).ToList();

            UserInterface.ShowContacts(filteredContacts, 0);
        }
    }
    internal static void InsertContact()
    {
        var contact = new Contact();
        Console.WriteLine("Enter 0 to go back to main menu");
        contact.Name = AnsiConsole.Ask<string>("Contact's name: ");
        if (contact.Name == "0") return;
        Console.Clear();
        contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phone number(format: +###########):");
        while (!PhoneBookValidation.ValidateNumber(contact.PhoneNumber))
        {
            contact.PhoneNumber = AnsiConsole.Ask<string>("Contact's phone number(format: +###########):");
        }
        contact.Email = AnsiConsole.Ask<string>("Contact's email: ");
        while (!PhoneBookValidation.ValidateEmail(contact.Email))
        {
            Console.WriteLine("Invalid Email. Use the following format (example@example.com)");
            contact.Email = AnsiConsole.Ask<string>("Contact's email: ");
        }
        contact.Category = AnsiConsole.Ask<string>("Contact Category: ");
        while (PhoneBookValidation.ValidateCategory(contact.Category))
        {
            Console.WriteLine("Category can't be null or a number");
            contact.Category = AnsiConsole.Ask<string>("Contact Category: ");
        }

        Controller.Insert(contact);
    }
    internal static void DeleteContact()
    {
        var contact = GetContactInput();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title($"Are you sure you want to delete {contact.Name} from contacts?")
        .AddChoices("Yes", "No"));
        if (option == "Yes")
        {
            Controller.Delete(contact);
            Console.WriteLine($"{contact.Name} deleted. Press enter to go back to main menu.");
            Console.ReadLine();
        }
        else if (option == "No")
            UserInterface.MainMenu();
    }
    internal static void UpdateContact()
    {
        var contact = GetContactInput();
        contact.Name = AnsiConsole.Confirm("Update name?")
        ? AnsiConsole.Ask<string>("Contact's new name: ")
        : contact.Name;

        contact.PhoneNumber = AnsiConsole.Confirm("Update Phone Number?")
        ? AnsiConsole.Ask<string>("Contact's new phone number: ")
        : contact.PhoneNumber;

        contact.Email = AnsiConsole.Confirm("Update email?")
        ? AnsiConsole.Ask<string>("Contact's new email: ")
        : contact.Email;

        contact.Category = AnsiConsole.Confirm("Update Category?")
        ? AnsiConsole.Ask<string>("Contact Category: ")
        : contact.Category;

        Controller.Update(contact);
    }
    public static Contact GetContactInput()
    {
        var contacts = Controller.Read();
        int id;

        var contactArray = contacts.Select(p => p.Name).ToArray();
        if (contactArray.Length == 0)
        {
            Console.WriteLine("No contacts available.");
            Console.WriteLine("Press enter to go back to main menu.");
            Console.ReadLine();
            UserInterface.MainMenu();
        }

        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Choose Contact")
        .AddChoices(contactArray));
        var multiContacts = contacts.Where(p => p.Name == option).ToList();
        if (multiContacts.Count() > 1)
        {
            do
            {
                Console.WriteLine("There are multiple contacts with the same name.");
                Console.WriteLine("Enter the id of the contact you want to choose: ");
                UserInterface.ShowContacts(multiContacts, 1);
                option = Console.ReadLine();
                Console.Clear();
            } while (PhoneBookValidation.ValidateString(option, multiContacts));

            id = multiContacts.Single(p => p.Id == int.Parse(option)).Id;
        }
        else
        {
            id = contacts.Single(p => p.Name == option).Id;
        }
        var contact = Controller.GetContact(id);

        return contact;
    }
    internal static void SendEmail()
    {
        try
        {
            string senderEmail = AnsiConsole.Ask<string>("Your email: ");
            Console.WriteLine("If you have 2 factor authentication you need an app password");
            string senderPassword = AnsiConsole.Ask<string>("Password: ");

            var contacts = Controller.Read();
            var emails = contacts.Select(p => p.Email).ToArray();
            var recipientEmail = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("Choose email")
            .AddChoices(emails));

            MailMessage message = new MailMessage(senderEmail, recipientEmail);

            message.Subject = AnsiConsole.Ask<string>("Mail subject: ");
            message.Body = AnsiConsole.Ask<string>("Message: ");
            SmtpClient[] smtpClients =
            {
                new SmtpClient("smtp.gmail.com"),
                new SmtpClient("smtp.office365.com"),
                new SmtpClient("smtp.mail.yahoo.com"),
                new SmtpClient("smtp.zoho.com"),
                new SmtpClient("smtp.fastmail.com"),
                new SmtpClient("smtp.gmx.com"),
                new SmtpClient("smtp.mail.com")
            };
            bool sentMail = false;
            foreach (var smtpClient in smtpClients)
            {
                int startingIndex = senderEmail.IndexOf('@') + 1;
                int indexOfDot = senderEmail.IndexOf('.');
                int length = indexOfDot - startingIndex;
                string mailClient = senderEmail.Substring(startingIndex, length);
                if (mailClient == "hotmail") mailClient = "office365";
                if (smtpClient.Host.Contains(mailClient))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;

                    smtpClient.Send(message);
                    Console.WriteLine("Email sent successfully!");
                    sentMail = true;
                }
            }

            if (sentMail == false)
            {
                Console.WriteLine("Email failed to send!");
                Console.WriteLine("Your domain might not be in our list. Try again using a different email.");
                Console.WriteLine("Press enter to go back to main menu.");
                Console.ReadLine();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
    internal static void SendSMS()
    {
        var contacts = Controller.Read();
        var numbers = contacts.Select(x => x.PhoneNumber).ToArray();
        Console.WriteLine("WARNING! You can only send SMS if you have a Twilio account created.");
        Console.WriteLine("Do you have a twilio account? (yes/no)");
        var option = Console.ReadLine();
        if (option == "yes")
        {
            string accountSid = AnsiConsole.Ask<string>("Your Twilio Account SID: ");
            string authToken = AnsiConsole.Ask<string>("Your Twilio Account Token: ");
            TwilioClient.Init(accountSid, authToken);

            string fromPhoneNumber = AnsiConsole.Ask<string>("Your Twilio Phone Number: ");
            string toPhoneNumber = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title("SMS Recipient Phone Number: ")
            .AddChoices(numbers));
            string messageBody = AnsiConsole.Ask<string>("Message: ");

            try
            {
                var message = MessageResource.Create(
                    body: messageBody,
                    from: new Twilio.Types.PhoneNumber(fromPhoneNumber),
                    to: new Twilio.Types.PhoneNumber(toPhoneNumber)
                );
                Console.WriteLine($"SMS sent successfully. Message SID: {message.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send SMS: {ex.Message}");
            }
        }
        else if (option == "no")
        {
            Console.WriteLine("Go to this website https://www.twilio.com/en-us and sign up to be able to send SMS.");
            Console.WriteLine("Press enter to go back to main menu.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("That is not an option!");
            Console.WriteLine("Press enter to go back to main menu");
            Console.ReadLine();
        }
    }
}
