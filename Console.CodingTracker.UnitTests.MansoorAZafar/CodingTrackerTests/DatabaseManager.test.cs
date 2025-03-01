using CodingTrackerLibrary.Controllers;
using CodingTrackerLibrary.Controllers.Database;
using CodingTrackerLibrary.Models;
using Dapper;
using Microsoft.Data.Sqlite;


namespace CodingTrackerTests;
[TestClass]
public class DatabaseManagerTest
{

    private DatabaseManager? _dbManager;
    private static SqliteConnection? _sharedConnection;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        const string testConfigurationFile = "appsettings.json";
        File.WriteAllText(testConfigurationFile,
            "{ \"ConnectionString\": \"Data Source=:memory:;Mode=Memory;Cache=Shared\" }");

        // Open a shared in-memory connection
        //  - Because the database manager code closes the connection
        //    need a shared connection for all db to access.
        //  - Need the connection stay open and not be .Closed() until the end
        _sharedConnection = new SqliteConnection("Data Source=:memory:;Mode=Memory;Cache=Shared");
        _sharedConnection.Open();
    }

    [TestInitialize]
    public void Setup()
        => _dbManager = new DatabaseManager();


    [TestMethod]
    public void Test_GetNumberOfEntries_WithDummyData()
    {
        const int expectedEntries = 5;
        int numberOfEntries = this._dbManager.GetNumberOfEntries();

        List<CodingSession> expectedCodingHabits = new List<CodingSession>
        {
            new ( id: 1, startDate:  "2020-01-16", endDate: "2020-01-17", duration: 24, units: "hours"),
            new ( id: 2, startDate:  "2020-01-18", endDate: "2020-01-20", duration: 48, units: "hours"),
            new ( id: 3, startDate:  "2020-01-21", endDate: "2020-01-24", duration: 72, units: "hours"),
            new ( id: 4, startDate:  "2020-01-25", endDate: "2020-01-29", duration: 96, units: "hours"),
            new ( id: 5, startDate:  "2021-01-13", endDate: "2021-01-18", duration: 120, units: "hours")
        };
        List<CodingSession> receivedCodingHabits = this._dbManager.GetAllData();

        
        Assert.AreEqual(expectedEntries, numberOfEntries);
        for (int i = 0; i < expectedCodingHabits.Count; ++i)
            Assert.AreEqual(expectedCodingHabits[i], receivedCodingHabits[i]);
    }


    [TestMethod]
    public void Test_GetAllEntries()
    {
        List<CodingSession> expectedCodingHabits = new List<CodingSession>
        {
            new ( id: 1, startDate:  "2020-01-16", endDate: "2020-01-17", duration: 24, units: "hours"),
            new ( id: 2, startDate:  "2020-01-18", endDate: "2020-01-20", duration: 48, units: "hours"),
            new ( id: 3, startDate:  "2020-01-21", endDate: "2020-01-24", duration: 72, units: "hours"),
            new ( id: 4, startDate:  "2020-01-25", endDate: "2020-01-29", duration: 96, units: "hours"),
            new ( id: 5, startDate:  "2021-01-13", endDate: "2021-01-18", duration: 120, units: "hours")
        };
        List<CodingSession> receivedCodingHabits = this._dbManager.GetAllData();


        for (int i = 0; i < expectedCodingHabits.Count; ++i)
            Assert.AreEqual(expectedCodingHabits[i], receivedCodingHabits[i]);
    }


    [TestMethod]
    public void Test_IdExists_WithInvalidID()
    {
        const bool expected = false;
        bool received = this._dbManager.IDExists(-5000000);

        Assert.AreEqual(expected, received);
    }

    [TestMethod]
    public void Test_IDExists_WithValidID()
    {
        const bool expected = true;
        bool received = this._dbManager.IDExists(1);

        Assert.AreEqual(expected, received);
    }

    [TestMethod]
    public void Test_Get_WithInvalidID()
        => Assert.ThrowsException<InvalidOperationException>(() => this._dbManager.Get(-1));

    [TestMethod]
    public void Test_Get_WithValidID()
    {
        CodingSession expected = new(id: 1, startDate: "2020-01-16", endDate: "2020-01-17", duration: 24, units: "hours");
        CodingSession received = this._dbManager.Get(1);

        Assert.AreEqual(expected, received);
    }


    [TestMethod]
    public void Test_Update_WithInvalidID()
    {
        int id = -1;
        DateTime startTime
            = DateTime.ParseExact
            (
                "3000-01-18-01",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        DateTime endTime
            = DateTime.ParseExact
            (
                "3000-01-19-08",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        TimeSpan duration
            = Utilities.CalculateDurationBetweenStartAndEndTime(startTime, endTime);

        this._dbManager.Update(ref id, ref startTime, ref endTime, duration);
        Assert.ThrowsException<InvalidOperationException>(() => this._dbManager.Get(-1));
    }

    [TestMethod]
    public void Test_Update_WithValidID()
    {
        int id = 1;
        DateTime expectedStartTime
            = DateTime.ParseExact
            (
                "3000-01-18-01",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        DateTime expectedEndTime
            = DateTime.ParseExact
            (
                "3000-01-19-08",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        TimeSpan expectedDuration
            = Utilities.CalculateDurationBetweenStartAndEndTime(expectedStartTime, expectedEndTime);
        this._dbManager.Update(ref id, ref expectedStartTime, ref expectedEndTime, expectedDuration);

        CodingSession expected = new(id, "3000-01-18", "3000-01-19", (float)expectedDuration.TotalHours, "hours");
        CodingSession received = this._dbManager.Get(1);

        Assert.AreEqual(expected, received);
    }

    [TestMethod]
    public void Test_Insert()
    {
        const int expectedID = 6;
        string expectedUnits = "hours";
        DateTime expectedStartTime
            = DateTime.ParseExact
            (
                "2025-01-05-05",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        DateTime expectedEndTime
            = DateTime.ParseExact
            (
                "2028-05-05-05",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        TimeSpan expectedDuration
            = Utilities.CalculateDurationBetweenStartAndEndTime(expectedStartTime, expectedEndTime);
        this._dbManager.Insert(ref expectedStartTime, ref expectedEndTime, expectedDuration);

        CodingSession expected 
            = new(expectedID, "2025-01-05", "2028-05-05", (float)expectedDuration.TotalHours, expectedUnits);
        CodingSession actual = this._dbManager.Get(expectedID);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Test_Delete_InvalidID()
    {
        int deleteID = -1;
        this._dbManager.Delete(ref deleteID);

        Assert.ThrowsException<InvalidOperationException>
            (() => this._dbManager.Get(deleteID));
    }

    [TestMethod]
    public void Test_Delete_ValidID()
    {
        int deleteID = 6;
        this._dbManager.Delete(ref deleteID);

        Assert.ThrowsException<InvalidOperationException>
            (() => this._dbManager.Get(deleteID));
    }


    [TestMethod]
    public void Test_GetDataFromDate()
    {
        DateTime expectedStartDate
            = DateTime.ParseExact
            (
                "2020-01-16-00",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        DateTime expectedEndDate
            = DateTime.ParseExact
            (
                "2020-01-24-23",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );

        List<CodingSession> expectedCodingHabits = new List<CodingSession>
        {
            new ( id: 1, startDate:  "2020-01-16", endDate: "2020-01-17", duration: 24, units: "hours"),
            new ( id: 2, startDate:  "2020-01-18", endDate: "2020-01-20", duration: 48, units: "hours"),
            new ( id: 3, startDate:  "2020-01-21", endDate: "2020-01-24", duration: 72, units: "hours"),
        };

        List<CodingSession> sessions = this._dbManager.GetDataFromDate(ref expectedStartDate, ref expectedEndDate);

        for (int i = 0; i < expectedCodingHabits.Count; ++i)
            Assert.AreEqual(expectedCodingHabits[i], sessions[i]);
    }

    [TestMethod]
    public void Test_GetTotalQuantity()
    {
        this.CleanupDatabase();

        int expectedTotalQuantity = 0;
        int actualTotalQuantity = this._dbManager.GetTotalQuantity();
        
        Assert.AreEqual(expectedTotalQuantity, actualTotalQuantity);


        this.ReAddData();
        
        expectedTotalQuantity = 360;
        actualTotalQuantity = this._dbManager.GetTotalQuantity();

        Assert.AreEqual(expectedTotalQuantity, actualTotalQuantity);
    }

    [TestMethod]
    public void Test_GetTotalQuantityFromMonth()
    {
        int expectedTotalQuantity = 0;
        int month = 0;
        Assert.ThrowsException<System.ArgumentOutOfRangeException>
            (() => this._dbManager.GetTotalQuantityFromMonth(ref month));


        this.CleanupDatabase();
        this.ReAddData();

        expectedTotalQuantity = 360;
        month = 1;
        int actualTotalQuantity = this._dbManager.GetTotalQuantityFromMonth(ref month);

        Assert.AreEqual(expectedTotalQuantity, actualTotalQuantity);
    }


    [TestMethod]
    public void Test_GetDataSortedBy_DESC()
    {
        SortingSelections expectedSortingSelection = SortingSelections.DESC;
        Period expectedPeriod = Period.Hours;

        List<CodingSession> expectedCodingHabits = new List<CodingSession>
        {
            new ( id: 5, startDate:  "2021-01-13", endDate: "2021-01-18", duration: 120, units: "Hours"),
            new ( id: 4, startDate:  "2020-01-25", endDate: "2020-01-29", duration: 96, units: "Hours"),
            new ( id: 3, startDate:  "2020-01-21", endDate: "2020-01-24", duration: 72, units: "Hours"),
            new ( id: 2, startDate:  "2020-01-18", endDate: "2020-01-20", duration: 48, units: "Hours"),
            new ( id: 1, startDate:  "2020-01-16", endDate: "2020-01-17", duration: 24, units: "Hours")
        };
        List<CodingSession> receivedCodingHabits 
            = this._dbManager.GetDataSortedBy(expectedSortingSelection, expectedPeriod);


        for (int i = 0; i < expectedCodingHabits.Count; ++i)
            Assert.AreEqual(expectedCodingHabits[i], receivedCodingHabits[i]);
    }


    [TestMethod]
    public void Test_GetDataSortedBy_ASC()
    {
        SortingSelections expectedSortingSelection = SortingSelections.ASC;
        Period expectedPeriod = Period.Hours;

        List<CodingSession> expectedCodingHabits = new List<CodingSession>
        {
            new ( id: 1, startDate:  "2020-01-16", endDate: "2020-01-17", duration: 24, units: "Hours"),
            new ( id: 2, startDate:  "2020-01-18", endDate: "2020-01-20", duration: 48, units: "Hours"),
            new ( id: 3, startDate:  "2020-01-21", endDate: "2020-01-24", duration: 72, units: "Hours"),
            new ( id: 4, startDate:  "2020-01-25", endDate: "2020-01-29", duration: 96, units: "Hours"),
            new ( id: 5, startDate:  "2021-01-13", endDate: "2021-01-18", duration: 120, units: "Hours")
        };
        List<CodingSession> receivedCodingHabits
            = this._dbManager.GetDataSortedBy(expectedSortingSelection, expectedPeriod);


        for (int i = 0; i < expectedCodingHabits.Count; ++i)
            Assert.AreEqual(expectedCodingHabits[i], receivedCodingHabits[i]);
    }


    [TestMethod]
    public void Test_GetHoursUntilGoal()
    {
        DateTime expectedStartDate
            = DateTime.ParseExact
            (
                "2020-01-16-00",
                "yyyy-MM-dd-HH",
                null,
                System.Globalization.DateTimeStyles.None
            );
        const int expectedDuration = 24;
        int actualDuration = this._dbManager.GetHoursUntilGoal(ref expectedStartDate);

        Assert.AreEqual(expectedDuration, actualDuration);
    }

    [TestMethod]
    public void Test_ClearDatabase()
    {
        int expectedQuantity = 5;
        int actualQuantity = this._dbManager.GetNumberOfEntries();

        Assert.AreEqual(expectedQuantity, actualQuantity);

        this._dbManager.ClearDatabase();
        expectedQuantity = 0;
        actualQuantity = this._dbManager.GetNumberOfEntries();

        Assert.AreEqual(expectedQuantity, actualQuantity);
    }


    [TestCleanup]
    public void Cleanup()
    {
        this.CleanupDatabase();
        this._dbManager = null;
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        if(File.Exists("appsettings.json")) 
            File.Delete("appsettings.json");

        _sharedConnection?.Close();
        _sharedConnection?.Dispose();
        _sharedConnection = null;
    }

    private void CleanupDatabase()
    {
        this._dbManager.ClearDatabase();
    }

    private void ReAddData()
    {
        this._dbManager = null;
        this._dbManager = new DatabaseManager(true);
    }
}
