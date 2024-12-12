# ConsoleCodingTracker
	An application that lets users keep track of their coding sessions.
# Given Requirements:
- [x] When the application starts, it should create a SQLite database if one isn’t present..
- [x] It should also create a table in the database to store coding session information.
- [x] You need to be able to insert, delete, update, and view your logged coding sessions. 
- [x] Handle all possible errors so that the application never crashes. 
- [x] The application should only be terminated when the user inserts q. 
- [x] You can only interact with the database using raw SQL (Dapper). You can’t use mappers such as Entity Framework
- [x] Use parameterized queries to prevent SQL Injection.
- [x] Use a configuration file that will contain a database path and connection strings.
- [x] Use the "Spectre.Console" library.
- [x] Create separate classes in different files (ex. UserInput.cs, CodingController.cs...)
- [x] Allow user the specific format you want the date and time to be logged (YYYY-MM-DD HH:MM).
- [x] The user should be able to input the start and end times manually.
- [x] When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
# Features

* SQLite Database Connection

	- The program uses a SQLite db connection to store and read information. 
	- If no database or table exists, they will be created once the program starts.

* A console based UI where users can navigate by entering an option from the Menu
	
	 * Available options:
	 * - a: Add a coding session
	 * - v: View a coding session
	 * - d: Delete a coding session
	 * - u: Update a coding session
	 * - s: View all coding sessions
	 * - q: Exit
	
	 * Example usage:
	 * Your option? q


* CRUD Operations

	- From the main menu users can create, read, update or delete entries. 
	- Dates inputted are checked to make sure they are in the correct and realistic format. 


* View Coding Session data output uses Specter library to output in a more pleasant way

| Option | Description                              |
|--------|------------------------------------------|
| a      | Add a coding session                     |
| v      | View a coding session                    |
| d      | Delete a coding session                  |
| u      | Update a coding session                  |
| s      | View all coding sessions                 |
| q      | Exit                                     |	

	- [spectreconsole Library](https://spectreconsole.net/cli/exceptions)

# Challenges
	
 - This was the first time working with specter library. I did not know there was such a thing as that one. The library has many options to choose from
 which makes the learning curve a bit steady. This is why, I make my time to learn a bit about it. Then start it working with it. It was such a greatexperience.
 - Ensured code robustness and maintainability by adhering to KISS and SOLID principles, and DI which extended the development time.
 - Taking the time to think before coding was benificial since removing a line of code could break the whole thing. So I had to take
 my time before implementing a new feature.
	
# Lessons Learned
- First-Time Use of Specter Library: This was my first experience working with the Specter library, which was a new tool for me. I was initially unaware of its existence,
	and its extensive options presented a learning curve. Taking the time to understand its features and capabilities was essential, and the process of learning and working with it
	turned out to be a valuable experience.

- Code Robustness and Maintainability: Ensuring the robustness and maintainability of the code involved adhering to the KISS (Keep It Simple, Stupid) and SOLID principles,
as well as employing Dependency Injection (DI). While these practices extended the development time, they significantly improved the code quality and maintainability.

- Thoughtful Coding: Taking the time to think through code changes before implementation proved beneficial. Removing or altering a line of code without careful consideration
	could lead to issues or break functionality.
	Therefore, a thoughtful approach to coding and feature implementation was crucial for maintaining a stable and functional application.
 
# Areas to Improve
- Deepen Understanding of the Specter Library: Although working with the Specter library was a valuable learning experience, there's still room to deepen my understanding of its advanced features
	and best practices. More in-depth knowledge could enhance my ability to leverage the library effectively in future projects.

- Mastering KISS and SOLID Principles: While I adhered to KISS and SOLID principles during development, continuously improving my application of these principles is essential.
	Further study and practical application can help in writing even cleaner, more efficient, and maintainable code.

- Enhance Planning and Design Skills: The experience underscored the importance of thorough planning and design before coding. Improving my skills in designing systems and planning features can prevent
	potential issues and make development more efficient. Investing more time in upfront design could minimize the need for extensive refactoring later.

- Expand Knowledge of Dependency Injection (DI): Although DI was used to improve code maintainability, expanding my understanding of different DI patterns and practices can further enhance my ability
	to design flexible and decoupled systems.

- Improve Error Handling and Testing: While error handling was a focus, there is always room for improvement. Developing more comprehensive testing strategies and improving error handling practices
	can help in creating more robust and reliable applications..

# Resources Used
 - [MS docs for setting up SQLite with C#](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
 - [Specter Library docs](https://spectreconsole.net/cli/exceptions)
 - [Dapper docs](https://www.learndapper.com/)
 - [MS Store Customer config file docs](https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/store-custom-information-config-file)
 - [DateTime Conversion article](https://medium.com/@Has_San/datetime-in-c-1aef47db4feb)
  