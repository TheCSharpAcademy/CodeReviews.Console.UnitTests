Hello

# Project:
    -> The project is a CLI CRUD application 
    -> The aim is to make an app to record and track coding hours
    -> The project uses local SQLite database for storage
    
# Architecture:
The project consist of 2 main project folder inside the solution 
    -Solution
        -Program
        -Library
the simply aims to make things much more cleaner and easier to navigate since it separates the main app logic and the database or the data structure (CodingSession)
to make it easier for further development .
the program file is separated into several files such as the main menu and input handler to prevent the mistake of the previous project of repeated code and with that the DRY principle is applied.
On the other side , there is the library where the CodingSession class and anything related to the database resides.

# Technology used:
    1. Dapper -> Entity framework built on top of ADO.NET
    2. ADO.NET -> provides SQLite functions to communicate with the databse
    3. Spectre.Console -> Cli tool to display the session records in a good looking table

# Things learned:
    -> Importance of DRY principle and its importance in reducing development time and making code much cleaner and easier to handle
    -> use of an Entity Framework (Dapper) 
    -> Using Spectre.Console to make a clean UI in the terminal
    -> using XML config files.
# Challenges:
    -> At the beginning the formatting of date time was issue but after reading the documentation I made a custom format for parsing and writing
    -> It seems that SQLite doesnt have a DATETIME variable type so i had to save dates as strings;
    -> No Prior knowledge about config files which was written in XML but it wasnt hard to figure it out in the end
# Resources:
    1. ChatGPT -> used mainly for learning basic SQL commands and learning about Dapper
    2. Spectre.Console Website -> https://spectreconsole.net

