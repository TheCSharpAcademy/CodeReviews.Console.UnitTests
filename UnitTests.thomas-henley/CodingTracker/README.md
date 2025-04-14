# Coding Tracker
#### Thomas Henley, C# Academy

## Introduction

This command line application can be used to time, track, and review coding sessions.

## Configuration

The program is configured in the `appsettings.json` file.

> [!Important]
> `ConnectionStrings.SQLite` must be set for the program to operate. All the other settings revert to sensible defaults if omitted.

```
{
    "ConnectionStrings": {
        "SQLite": "Data Source=CodingTracker.db;"
    },
    "TableName": "CodingSessions",

    /* Other datetime formats are not currently supported. */
    "DateTimeFormat": "yyyy-MM-dd HH:mm",

    /* Replace existing records in the database with randomly generated data. */
    "UseExampleData": false,

    /* Destroy the table upon exiting the program. Used for testing and demonstration. */
    "TeardownData": false
}
```


## Usage

> [!Important]
> The database specification and other options are set in the configuration file. Please read the configuration section first.

Users will see a menu upon launching the application:

```
CODING TRACKER

What would you like to do?

> Add Session
  Start New Timed Session
  Review Sessions
  Edit Session
  Delete Session
  Delete All Sessions
  Exit
```

The program utilizes the Spectre console library for console interaction. Menus are navigated with the arrow and Enter keys.

1. Add Session
   - Prompts the user for a start and end time of a session to be added to the database.
1. Start New Timed Session
   - Starts a timer in the console. When the user presses "Q", the timer will end and the session will be recorded in the database.
1. Review Sessions
   - Allows the user to view all the records in a table, or filter them by year/month/duration.
1. Edit Session
   - Prompts the user for the ID of a recorded session and allows them to update the start and end time.
1. Delete Session
   - Prompts the user for the ID of a recorded session to delete from the database. Requires confirmation.
1. Delete All Sessions
   - Deletes all recorded sessions from the database. Requires confirmation.
1. Exit
   - Exits the program.

## Unit Testing

The solution includes a `CodingTracker.Tests` project. To run the test suite, open the Test Explorer window in Visual Studio and Run All Tests.

> [!Note]
> The test project includes its own `testsettings.json` file but there is no need to alter it.

Currently this project exercises the following classes:
- CodingSession
- DapperHelper
- SpectreValidation

The following classes are not yet tested:
- CodingController
- SpectreConsole
