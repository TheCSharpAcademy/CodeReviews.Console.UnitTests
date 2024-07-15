using System.Net.Mail;
using PhoneNumbers;

namespace PhoneBook;

public static class PhoneBookValidation
{
    private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();
    public static bool ValidateNumber(string phoneNumber)
    {
        bool valid = false;
        try
        {
        var number = phoneNumberUtil.Parse(phoneNumber, null);
        if (phoneNumberUtil.IsValidNumber(number)) valid = true;
        }
        catch(Exception ex)
        {
            return valid;
        }
        return valid;

    }
    public static bool ValidateEmail(string email)
    {
        try
        {
            MailAddress mailAddress = new MailAddress(email);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public static bool ValidateCategory(string? option)
    {
        var isNumber = int.TryParse(option, out _);
        return isNumber;
    }

    public static bool ValidateString(string userInput, List<Contact> contacts)
    {
        var isNull = string.IsNullOrEmpty(userInput);
        if (isNull)
        {
            Console.WriteLine("Invalid Id! Try again.");
            return isNull;
        }
        foreach (var contact in contacts)
        {
            var isNum = int.TryParse(userInput, out _);
            if (isNum == false)
            {
                Console.WriteLine("That's not a number! Try again.");
                return true;
            }
            if (int.Parse(userInput) == contact.Id)
            {
                isNull = false;
                break;
            }
            else
                isNull = true;
        }
        if (isNull)
        {
            {
                Console.WriteLine("Invalid Id. Try again.");
                return true;
            }
        }
        return isNull;
    }
}