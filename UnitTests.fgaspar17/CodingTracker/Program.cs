using CodingTrackerLibrary;
using CodingTracker;

SetupDatabase setupDatabase = new SetupDatabase(Settings.GetConnectionString());
setupDatabase.InitializeDatabase();

#if DEBUG
setupDatabase.SeedData();
#endif

Application app = new Application();
app.Run(Settings.GetConnectionString());