using CodingTrackerLibrary.Controllers.Database;
using CodingTrackerLibrary.Models;
using CodingTrackerLibrary.Views;

namespace CodingTrackerLibrary.Controllers;

internal class CodingHabitManager
{
    private DatabaseManager databaseManager = new();
    private Reports? reports;
    private bool? codingGoalExist;
    private DateTime startingCodingGoalDate;
    private int codingGoalHours;

    public void DoMenuAction(MenuSelections chosenSelection)
    {
        Console.Clear();
        int id = -1;

        switch(chosenSelection)
        {
            case MenuSelections.update:
                DataViewer.DisplayHeader("Updating a Coding Habit");
                
                this.UpdateCodingHabit(ref id);
                break;
            
            case MenuSelections.delete:
                DataViewer.DisplayHeader("Deleting a Coding Habit");
                
                this.DeleteCodingHabit(ref id);
                break;
            
            case MenuSelections.insert:
                DataViewer.DisplayHeader("Inserting A Coding Habit");
                
                this.InsertCodingHabit();
                break;
            
            case MenuSelections.data:
                DataViewer.DisplayHeader("Viewing All Data");
                DataViewer.DisplayListAsTableLive<CodingSession>(CodingSession.headers, this.databaseManager.GetAllData());
                
                Utilities.PressToContinue();
                break;
            
            case MenuSelections.reports:
                DataViewer.DisplayHeader("Viewing Reports");
                
                if (this.reports == null) 
                    this.reports = new Reports(this.databaseManager);
                
                this.reports.HandleReportSelection();
                break;

            case MenuSelections.goals:
                if(this.reports == null) 
                    this.reports = new Reports(this.databaseManager);
                
                DataViewer.DisplayHeader("Coding Goal");
                this.HandleCodingGoal();
                
                break;

            default:
                break;
        }
        System.Console.Clear();
    }

    private void AssignValidID(ref int id)
    {
        do
        {
            Utilities.GetValidIntegerInput
            (
                input: ref id,
                message: "Please enter an Existing ID\n> ",
                errorMessage: "Invalid Answer\n Please Enter a valid ID\n> "
            );
        } while (!this.databaseManager.IDExists(id));
    }

    private bool EntriesExist()
    {
        return this.databaseManager.GetNumberOfEntries() > 0;
    }

    private void DeleteCodingHabit(ref int id)
    {
        if (!this.EntriesExist())
        {
            Console.WriteLine("There are no Entries in the Database\n" +
                "There is nothing to delete\n");

            Utilities.PressToContinue();
            return;
        }

        DataViewer.DisplayHeader("Current Data", "left");
        DataViewer.DisplayListAsTable(CodingSession.headers, this.databaseManager.GetAllData());

        this.AssignValidID(ref id);
        this.databaseManager.Delete(id: ref id);
    }

    private void UpdateCodingHabit(ref int id)
    {
        if (!this.EntriesExist())
        {
            Console.WriteLine("There are no Entries in the Database\nThere is nothing to update\n");
            Utilities.PressToContinue();
            return;
        }

        DataViewer.DisplayHeader("Current Data", "left");
        DataViewer.DisplayListAsTable(CodingSession.headers, this.databaseManager.GetAllData());

        this.AssignValidID(ref id);

        DateTime startDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref startDate,
            message: "Please enter the Starting Date in yyyy-MM-dd-HH (ex: 2024-02-19-13)\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Date (yyyy-MM-dd-HH)\n> "
        );

        DateTime endDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref endDate,
            message: "Please enter the Ending Date in yyyy-MM-dd-HH (ex: 2024-02-19-01)\n> ",
            errorMessage: $"Invalid Answer\nPlease Enter a valid Ending Date After {startDate:yyyy-MM-dd-HH} as (yyyy-MM-dd-HH)\n> ",
            condition: d => d >= startDate
        );

        this.databaseManager.Update
        (
            ID: ref id,
            startDate: ref startDate,
            endDate: ref endDate,
            duration: Utilities.CalculateDurationBetweenStartAndEndTime(startDate,endDate)
        );
    }

    private void InsertCodingHabit()
    {
        if (!this.EntriesExist())
        {
            Console.WriteLine("There are no Entries in the Database\nThere is nothing to update\n");
            Utilities.PressToContinue();
            return;
        }

        DateTime startDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref startDate,
            message: "Please enter the Starting Date in yyyy-MM-dd-HH (ex: 2024-02-19-23)\n> ",
            errorMessage: "Invalid Answer\nPlease Enter a valid Date (yyyy-MM-dd-HH)\n> "
        );

        DateTime endDate = DateTime.Now;
        Utilities.GetValidDateInyyMMddHHFormat
        (
            input: ref endDate,
            message: "Please enter the Ending Date in yyyy-MM-dd-HH (ex: 2024-02-19-01)\n> ",
            errorMessage: $"Invalid Answer\nPlease Enter a valid Ending Date After {startDate:yyyy-MM-dd-HH} as (yyyy-MM-dd-HH)\n> ",
            condition: d => d >= startDate
        );

        this.databaseManager.Insert
        (
            startDate: ref startDate,
            endDate: ref endDate,
            duration: Utilities.CalculateDurationBetweenStartAndEndTime(startDate, endDate)
        );
    }

    private void HandleCodingGoal()
    {
        if (codingGoalExist != true)
        {
            codingGoalExist = true;

            Utilities.GetValidIntegerInput
            (
                input: ref this.codingGoalHours,
                message: "Please Enter a valid amount of hours in a day\n> ",
                errorMessage: "Invalid Answer, Choose between 1-24 hours\n> ",
                lowRange: 1,
                maxRange: 24
            );
            
            Utilities.PressToContinue();
            System.Console.Clear();

            Utilities.GetValidDateInyyMMddHHFormat
            (
                input: ref startingCodingGoalDate,
                message: "Please enter the Starting Date in yyyy-MM-dd-HH (ex: 2024-02-19-23)\n> ",
                errorMessage: "Invalid Answer\nPlease Enter a valid Date (yyyy-MM-dd-HH)\n> "
            );
            Utilities.PressToContinue();
        }

        int totalTimeDone = this.databaseManager.GetHoursUntilGoal(ref startingCodingGoalDate);

        System.Console.Clear();
        DataViewer.DisplayHeader("Coding Goal");
        DataViewer.DisplayHeader($"Time Left To Complete on {this.startingCodingGoalDate:yyyy-MM-dd}", "left");
        System.Console.WriteLine($"{this.codingGoalHours - totalTimeDone}\n");

        Utilities.PressToContinue();
    }
}
