﻿using CodingTracker.SamGannon.Models;
using CodingTracker.SamGannon.Utility;
using System;
using System.Linq;

namespace CodingTracker.SamGannon.UserInterface;

internal class SleepMenu
{
    UserInputHelper helper = new();
    Validation validator = new();

    internal void ShowSleepMenu()
    {
        Console.Clear();
        bool closeApp = false;
        while (closeApp == false)
        {
            Console.WriteLine("-----Sleep Menu-----");
            Console.WriteLine("What would you like to do? Press the corresponding number key:");
            Console.WriteLine("1 - View All Records");
            Console.WriteLine("2 - Add A Record");
            Console.WriteLine("3 - Delete A Record");
            Console.WriteLine("4 - Update A Record");
            Console.WriteLine("0 - Close Application");

            var userCommand = Console.ReadLine();

            while (string.IsNullOrEmpty(userCommand))
            {
                Console.WriteLine("\nInvalid Command type a number 0 to 4.\n");
                userCommand = Console.ReadLine();
            }

            switch (userCommand)
            {
                case "0":
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    CodingController codingController = new();
                    codingController.GetSleepData();
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadLine();
                    break;
                case "2":
                    ProcessSleepAdd();
                    break;
                case "3":
                    DeleteSleepRecord();
                    break;
                case "4":
                    UpdateSleepRecord();
                    break;
                default:
                    Console.WriteLine("Invalid Commmand. Press any key and enter to continue");
                    Console.ReadLine();
                    break;
            }
        }
    }


    private void ProcessSleepAdd()
    {
        var startTime = helper.GetStartTime();
        var endTime = helper.GetEndTime();
        var duration = helper.CalculateDuration(startTime, endTime);

        Sleep sleep = new();

        sleep.Duration = duration;
        sleep.SleepType = helper.CalculateSleepType(sleep.Duration);

        CodingController codingController = new();
        codingController.PostSleep(sleep);
    }

    private void DeleteSleepRecord()
    {
        CodingController codingController = new();
        codingController.GetSleepData();
        Console.WriteLine("Please add id of the record you want to delete (or press 0 to reutrn to Main Menu).");

        string commandInput = Console.ReadLine();

        var id = helper.ValidateIdInput(commandInput);

        GetUserInput getUserInput = new();
        if (id == 0) getUserInput.MainMenu();

        var sleep = codingController.GetBySleepId(id);

        while (sleep.Id == 0)
        {
            Console.WriteLine($"\nRecord with id {id} doesn't exist\n");
            DeleteSleepRecord();
        }

        codingController.DeleteSleep(id);
    }

    private void UpdateSleepRecord()
    {
        CodingController codingController = new();
        codingController.GetSleepData();

        Console.WriteLine("Please add id of the record you want to update (or 0 to return to the Main Menu).");
        string commandInput = Console.ReadLine();

        var id = helper.ValidateIdInput(commandInput);

        GetUserInput getUserInput = new();
        if (id == 0) getUserInput.MainMenu();

        var sleep = codingController.GetBySleepId(id);

        while (sleep.Id == 0)
        {
            Console.WriteLine($"\nRecord with Id {id} doesn't exist\n");
            UpdateSleepRecord();
        }

        var updateInput = "";

        bool updating = true;
        while (updating == true)
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine($"Type 'u' to update record");
            Console.WriteLine($"Type '0' to go back to the Main Menu");

            updateInput = Console.ReadLine();

            switch (updateInput)
            {
                case "u":
                    var startTime = helper.GetStartTime();
                    var endTime = helper.GetEndTime();
                    sleep.Duration = helper.CalculateDuration(startTime, endTime);
                    sleep.SleepType = helper.CalculateSleepType(sleep.Duration);
                    updating = false;
                    break;
                case "0":
                    getUserInput.MainMenu();
                    break;
                default:
                    Console.WriteLine($"\nType '0' to go back to the Main Menu");
                    break;

            }
        }
        codingController.UpdateSleep(sleep);
        Console.WriteLine("Record has been updated. Press a key to continue");
        Console.ReadLine();
        getUserInput.MainMenu();
    }
}
