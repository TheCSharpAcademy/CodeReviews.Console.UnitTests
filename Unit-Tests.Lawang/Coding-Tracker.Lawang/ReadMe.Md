
# Coding-Tracker.Lawang

This is the Console based CRUD application to store and track your coding session using .Net environment and Dapper as micro ORM to interact
sqlite as Database. 

## Requirements

- When the application starts, it should create a sqlite database, if one isn’t present.
 
- It should also create a table in the database, where the data is seeded custom data is sedded if the table is empty.
- You need to be able to insert, delete, update and view your logged coding session.
- You should handle all possible errors so that the application never crashes
- You can only interact with the database using Dapper.

## Features
* Can simply be navigated by pressing the "UP" and "DOWN" keys, moreover the interface is self explainatory.
* SQLite database is used to store and read data from.





* #### Screen shots:


![Screenshot from 2024-08-26 15-20-55](https://github.com/user-attachments/assets/454413fa-6d71-41e4-94f7-c0ad80dd358e)


* Users can Perform CRUD function on the database.
* Users can enter time in (hh:mm:ss) format and enter the date in the format (dd/mm/yy).

  ![Screenshot from 2024-08-16 13-50-32](https://github.com/user-attachments/assets/6ab704f4-150c-46df-aa89-b8a79eb8e583)


- Data is presented to user in Table format, using the external library Spectre.Console.
- This app is beautified using Spectre.Console.



## Project Summary
#### What challenges did you face and how did you overcome them?

* The requirement of this projecct was somewhat similar to what i did in previous project of habit tracker so to implement the CRUD operation on this application was lot easier.

  * To interact with the database we needed something in .net to interact with database. so, as suggested in Project I started learning about Dapper.
  .
* Spaghetti code was the issue when I tried to put all code inside Program.cs, I started making different classes for handling different section of the application.

* To follow the Separtion Of Concern principle, I made CodingController class for handling database operation, like wise Visual class to handle the gui of the application, UserInput class for checking the UserInput of the users input and lot more.


* Dealing with DateTime was a hassle, Project suggested me to use DateTime and Used TimeSpan to store the duration, learning the DateTime property method and properties helped me understand Timespan and use it easily.

* In Challeges section of this project we had to implement the timer, I implemented it using the timer class which was an interesting experience because I had never used the timer class and it introduced me to the concept of Events and delegates, but I have to learn on this more.




## 🛠 Skills Learned
#### DAPPER
* In my previous project with habit tracker I worked with ADO.NET where the C# code interact with database on a base level, In this project Dapper adds a micro level of abstraction on interacting with database, and honestly was much better option to use than plain ADO.NET.  

#### Spectre.Console
* As mentioned above, My own custom class was hedious to look at as it was hard to achieve alignment among different record. So, I learned to build table in Spectre.Console. Although I am not proficcient in using this terminal library but for now building the table is enough and hope to learn more and implement in future projects.

#### SQL
* To interact with database in this project using Dapper I needed to use raw SQL statement, and had to learn and implement it for basic operation. Using Dapper was a lot easier then implementing the ADO.NET

#### Events and Delegates
* As mentioned above to implement I got to introduced to Events and Delegates, now I have to dig deepeer into it.


## FAQ

#### How to beautify the table in the project?

Answer I used the Microsoft.Spectre.Console package, which you can get for Nuget package manager. Install it and add Reference to your project. 

For more information u can visit the docs https://spectreconsole.net




## Feedback

If you have any feedback, please reach out to us at depeshgurung44@gmail.com
