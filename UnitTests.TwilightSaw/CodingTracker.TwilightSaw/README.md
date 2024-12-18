# ConsoleHabitTracker

Console based CRUD application to track people habits. Developed using C# and SQLite with ADO.NET.

## Given Requirements:
- [x] This is an application where you log your daily coding time.
- [x] Users need to be able to input the date, time of start and end of the session.
- [x] "Spectre.Console" library to show the data on the console.
- [x] Have separate classes in different files.
- [x] Create a configuration file that you'll contain your database path and connection strings.
- [x] Create a "CodingSession" class in a separate file.
- [x] Tell the user the specific format you want the date and time to be logged and not allow any other format.
- [x] Duration of the session should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- [x] Reading from the database, read your table into a List of Coding Sessions.
- [x] The application should store and retrieve data from a real database.
- [x] When the application starts, it should create a sqlite database, if one isn’t present.
- [x] It should also create a table in the database, where the sessions will be stored.
- [x] The users should be able to insert, delete, update and view their sessions.
- [x] Should be handled all possible errors so that the application never crashes.
- [x] Iteraction can only be with the database using Dapper ORM.

## Features
* SQLite database connection with ADO.NET

* The program uses a SQLite db connection to store and read information.
  - If no database exists, or the correct table does not exist they will be created on program start.

* A console based UI where users can navigate by user input.
   ![image](https://github.com/TwilightSaw/CodeReviews.Console.CodingTracker/blob/main/images/UI.jpg?raw=true)
  
* CRUD DB functions
  - From the main menu users can Create, Read, Update or Delete entries for whichever date and time they want, entered in dd.mm.yyyy and hh:mm:ss format.

* Reports of total session amount, hours and average amount of time per session.
   ![image](https://github.com/TwilightSaw/CodeReviews.Console.CodingTracker/blob/main/images/report.png?raw=true)
    
* Goal report, where user can define his everyday time of coding and check if he succeed or not with progress bar.
   ![image](https://github.com/TwilightSaw/CodeReviews.Console.CodingTracker/blob/main/images/goal_report.jpg?raw=true)

* Check records that had been already created, filter and order ascending or descending.
   ![image](https://github.com/TwilightSaw/CodeReviews.Console.CodingTracker/blob/main/images/filter.png?raw=true)

## Challenges
- Validation of time is hard enough process, especially if you add timer to this. Parsing to DateTime, string and TimeSpan in a circle can be firstly challenging.
- Creating your first config file was a little confusing, but managable.
- Seperating code to different classes for SRP was not an easy task, and I think it could be done better, as I suspect not all the code was seperated correctly equally. But in future with help of something called delegates I suppose it would get better.
## Lessons Learned
- Spectre is much more powerful tool then just a Console.Write, but not as flexible as I thought.
- Dapper ORM commands are pretty much similar to ADO.NET, but a little more comfortable.
- Delegates is a whole new area for better coding and seperating code that is yet to be discovered.
## Areas to Improve
- Better SRP usage, OOP knowledge, KISS and DRY principles, validation methods.
## Resources Used
- C# Academy guidelines and roadmap.
- ChatGPT for new information as delegates, lambda operators, config creation etc..
- Spectre.Console documentation.
- Various StackOverflow articles.
