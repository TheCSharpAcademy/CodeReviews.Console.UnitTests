namespace PhoneBook;

internal static class Controller
{
    internal static void Delete(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Remove(contact);
        db.SaveChanges();
    }

    internal static Contact GetContact(int id)
    {
        using var db = new PhoneBookContext();
        var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
        return contact;
    }

    internal static void Insert(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Add(contact);
        db.SaveChanges();
    }

    public static List<Contact> Read()
    {
        using var db = new PhoneBookContext();
        var contacts = db.Contacts.ToList();
        return contacts;
    }

    internal static void Update(Contact contact)
    {
        using var db = new PhoneBookContext();
        db.Update(contact);
        db.SaveChanges();
    }
}