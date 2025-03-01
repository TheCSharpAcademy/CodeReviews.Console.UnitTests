# ConsoleHabitTracker
My second C# application with CRUD operations.

Console based CRUD application to track coding habits.
Developed using C#, Dapper, SQLite and Spectre.Console.

# Given Requirements
- [x] This application has the same requirements as the previous project, except that now you'll be logging your daily coding time.
- [x] To show the data on the console, you should use the "Spectre.Console" library.
- [x] You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)
- [x] You should tell the user the specific format you want the date and time to be logged and not allow any other format.
- [x] You'll need to create a configuration file that you'll contain your database path and connection strings.
- [x] You'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration
- [x] The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- [x] The user should be able to input the start and end times manually.
- [x] You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
- [x] When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.

# Features
* SQLite database connection

	- The program uses a SQLite database connection to store and read information
	- If no database exists, or the current table doesn't exist, they will be created on the program start.

* A console based UI where users can naviagted by moving with the array keys and pressing [ENTER]
  ![image](https://github.com/user-attachments/assets/3f1c7580-85c0-4f8e-be80-a05998156bcd)

* CRUD DB functions

- From the main menu users can Create, Read, Update or Delete entries.
- Time and Dates inputted are checked to make sure they are in the correct and realistic format.
  
* Basic Reports of Cumulative Hours

![image](https://github.com/user-attachments/assets/23b9b6d7-3211-4c99-ae06-bfb01a2d92cb)


* Reporting and other data output uses ConsoleTableExt library to output in a more pleasant way

![image](https://github.com/user-attachments/assets/6a35188c-6256-4095-bb1a-48a49a913721)


# Challenges

- I had some trouble figuring out setting up the Dapper ORM and understanding how it interacts with SQLite but after reading a bit of the docs, it was easy to understand and follow
- Figuring out how to ensure that the DateTime was only inputted in one format took some time to figure out but is surprisingly easy
- Figuring out how to organize my code and trying to follow MVC architecture was difficult

# Lessons Learned

- I learnt about Dapper ORM
- I learnt about Spectre.Console
- I learnt more about SQLite and casting with it
- Got a better understandng of C#'s syntax and features
- Better understood the Object Orienated Programming Paradigm

# Areas to Improve

- - I want to learn more of the shortcuts and keybinds of Visual Studio Code as I want to be able to traverse through files without clicking with my mouse or moving throughout the code more efficiently
- Single responibility, I'm not too sure I followed it properly as I don't have enough experience but think I could have made my programs more seperated
- I could have better utilized OOP to store the values and more seperate the habits 

# Resources used

- [Habit Tracker App. C# Beginner Project. CRUD Console, Sqlite, VSCode](https://www.youtube.com/watch?v=d1JIJdDVFjs)
- [Get datetime value from X days go](https://stackoverflow.com/questions/14008778/get-datetime-value-from-x-days-go)
- [SQLite date() Function](https://www.sqlitetutorial.net/sqlite-date-functions/sqlite-date-function/)
- [C# KISS Principle (Keep It Simple, Stupid!)](https://www.bytehide.com/blog/kiss-principle-csharp)
- [Datatypes In SQLite](https://www.sqlite.org/datatype3.html)
- [Learn Dapper](https://www.learndapper.com/)
- [Spectre Console](https://spectreconsole.net/)
