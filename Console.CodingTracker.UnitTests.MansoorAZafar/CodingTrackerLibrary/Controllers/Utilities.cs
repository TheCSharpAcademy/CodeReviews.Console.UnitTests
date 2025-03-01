using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace CodingTrackerLibrary.Controllers;
static internal class Utilities 
{
    private static readonly string? connectionString;

    public static void PressToContinue()
    {
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static TimeSpan CalculateDurationBetweenStartAndEndTime(DateTime start, DateTime end)
    {  
        return end - start;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static String? GetConnectionString()
    {
        return connectionString is null ? new ConfigurationBuilder()
                                              .AddJsonFile("appsettings.json", false, false)
                                              .Build()["ConnectionString"] : connectionString;
    }

    public static void GetValidIntegerInput
      (
        ref int input, 
        string message="> ", 
        string errorMessage="Invalid Value\n> ", 
        int lowRange = int.MinValue, 
        int maxRange = int.MaxValue
      )
    {
        System.Console.Write(message);
        while (!int.TryParse(System.Console.ReadLine(), out input) || (input < lowRange || input > maxRange))
            System.Console.Write(errorMessage);
    }

    public static void GetValidStringInput(ref string? input, string message = "> ", string errorMessage = "Invalid Input\n Please Enter the data\n> ")
    {
        Console.Write(message);
        input = System.Console.ReadLine();
        while (string.IsNullOrEmpty(input))
        {
            Console.Write(errorMessage);
            input = System.Console.ReadLine();
        }
    }

    public static void GetValidDateInyyMMddHHFormat(ref DateTime input, string message = "> ", string errorMessage = "Invalid Date, Please enter in yy-MM-dd Format\n> ", Func<DateTime, bool>? condition = null)
    {
        System.Console.Write(message);
        while(!DateTime.TryParseExact(System.Console.ReadLine(), 
                                      "yyyy-MM-dd-HH", 
                                      null, 
                                      System.Globalization.DateTimeStyles.None, 
                                      out input)
            || (condition != null && !condition(input)))
        {
            System.Console.Write(errorMessage);
        }
    }
}