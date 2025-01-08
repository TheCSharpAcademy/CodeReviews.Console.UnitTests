using Alvind0.CodingTracker.Controllers;
using Alvind0.CodingTracker.Data;
using Alvind0.CodingTracker.Views;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(Path.Combine("Alvind0.CodingTracker", "Properties", "appsettings.json"), optional: false, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"]
    ?? throw new NullReferenceException("Connection String does not exist in the configuration file.");

var sessionRepository = new CodingSessionRepository(connectionString);
var goalRepository = new GoalRepository(connectionString);
var renderer = new TableRenderer();
var sessionController = new CodingSessionController(sessionRepository, renderer);
var goalController = new GoalController(goalRepository, sessionRepository, renderer);
var menu = new Menu(sessionController, goalController);

sessionRepository.CreateTableIfNotExists();
goalRepository.CreateTableIfNotExists();
menu.MainMenu().Wait();
