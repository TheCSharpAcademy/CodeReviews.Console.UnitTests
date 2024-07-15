using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PhoneBook;

internal class PhoneBookContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
    }
}