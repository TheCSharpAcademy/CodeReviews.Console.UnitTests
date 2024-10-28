
# CodingTracker

A console-based application to track your progress in coding per day. Developed
using C#, Dapper, Spectre.Console and SQLite.

## Given Requirements

- [x] To show the data on the console, you should use the "Spectre.Console" library.
- [x] You're required to have separate classes in different files (ex. UserInput.cs,
Validation.cs, CodingController.cs)
- [x] You should tell the user the specific format you want the date and time to be logged
and not allow any other format.
- [x] You'll need to create a configuration file that you'll contain your database path
and connection strings.
- [x] You'll need to create a "CodingSession" class in a separate file. It will contain
the properties of your coding session: Id, StartTime, EndTime, Duration
- [x] The user shouldn't input the duration of the session. It should be calculated based
on the Start and End times, in a separate "CalculateDuration" method.
- [x] The user should be able to input the start and end times manually.
- [x] You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement
was included in Feb/2024)
- [x] When reading from the database, you can't use an anonymous object, you have to read
your table into a List of Coding Sessions.

## Features

- SQLite database connection
  - The data is stored in a db file which I connect to perform CRUD and analysis operations.
  - If the database doesn't exist, it creates one.
  - In DEBUG mode, it creates 20 random generated coding sessions records.

- Console based UI to navigate the menus

  - ![image](https://github.com/user-attachments/assets/328be0a0-b766-44f9-9a85-1bfd9ca399a5)
  - ![image](https://github.com/user-attachments/assets/12d74f11-e870-41f9-bcab-c5c9a52fc6a4)

- CRUD operations

  - From the first menu you can create, show or delete coding sessions.
  - From the second menu you can create, update, show or delete goals. To choose an option you make use of arrow keys and enter, for the day you enter the date in format YYYY-MM-DD HH:mm.
  - Showing coding sessions has two menus, one for the period(daily, weekly, yearly, all) and another for the ordering(ascending or descending).
  - ![image](https://github.com/user-attachments/assets/8fada7ff-160c-4c64-9eed-7acb3ec2a860)
  - ![image](https://github.com/user-attachments/assets/3a50c2aa-271c-4714-a109-4e1d3c74f8d9)
  - Inputs are validated to be the requested type.

- Stopwatch

  - ![image](https://github.com/user-attachments/assets/9fa0b6b5-c6db-4386-9ad1-8e877d3b1583)
  - Allows you to capture live sessions.

- Report functionality

  - ![image](https://github.com/user-attachments/assets/12434efd-2723-4035-bca6-392e7d021729)
  - You can choose a daily, weekly, yearly or an all records report.

- Goals Analysis

  - You can check the hours per day needed for a goal and the hours left to complete.
  - ![image](https://github.com/user-attachments/assets/7bfea428-f369-4ec6-ae09-4dd45c66cac0)

- Test

  - You can run `dotnet test`, so you can test the DateTime validation.

## Challenges

- Creating a configuration file.
- Working with DateTime and TimeSpan for dates in C#, validation and formatting.
- Another related challenge is working with them with SQLite.
- How to present Enum options in an understandable way for the users.
- Using Spectre.Console for a better UI.

## Lessons Learned

- SQLite functions round(), julianday() and strftime() are really helpful
for date operations.
- TimeSpan and DateTime give you many functions to solve problems for dates
and time intervals.
- Dapper capabilities reduce code complexity for mapping models and query
results.
- Spectre.Console is a powerful library to enrich user experience in console
apps.
- Attributes and extension methods allowed me to present enum fields for the
menus in a scalable and intuitive way.
- To map unsupported types by Dapper, you can use the dynamic type and convert
manually from double to TimeSpan, for example.
- Reflection is powerful but the performance cost is high, so you should cache
the properties via a dictionary.

## Areas to Improve

- Code duplication due to menu, I tried to reuse code from Habit Logger but Console.Spectre makes everything related to prompt and validation easier, so I should have created functions for the prompt types and not a menu for every prompt.
- DateTime and TimeSpan functions to help solve time problems in the easiest way.
- Reflection to improve application scalability.

## Resources used

 - StackOverflow posts
 - [Sqlite julianday()](https://www.sqlitetutorial.net/sqlite-date-functions/sqlite-julianday-function/)
 - [Sqlite strftime()](https://www.sqlitetutorial.net/sqlite-date-functions/sqlite-julianday-function/)
 - [Spectre Console Documentation](https://www.sqlitetutorial.net/sqlite-date-functions/sqlite-julianday-function/)
 - [Learn Dapper](https://www.learndapper.com/)
 - [TimeSpan Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.timespan?view=net-8.0)
 - [DateTime Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.datetime?view=net-8.0)