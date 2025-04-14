using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingTracker;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data.Common;

namespace CodingTracker.Tests;

[TestClass]
public class DapperHelperTests
{
    private IConfiguration _config = null!;
    private DapperHelper _dapper = null!;
    private SqliteConnection _testConnection = null!;
    private readonly string _table = "TestTable";

    private int GetRecordCount()
    {
        return _testConnection.ExecuteScalar<int>($"SELECT COUNT() FROM (SELECT id FROM {_table});");
    }

    private static CodingSession TestSessionA() => new("2024-08-14 01:00", "2024-08-14 02:05");
    private static CodingSession TestSessionB() => new("2024-08-15 10:15", "2024-08-15 14:22");
    private static CodingSession TestSessionC() => new("2024-08-16 20:00", "2024-08-16 22:10");

    [TestInitialize]
    public void TestInitialize()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json", true, true)
            .Build();

        _dapper = new DapperHelper(_config);
        _testConnection = new SqliteConnection(_config.GetConnectionString("SQLite"));

        if (_dapper.IsTableCreated())
        {
            _dapper.TeardownDB();
        }
        Assert.IsFalse(_dapper.IsTableCreated());
        _dapper.InitializeDb();
        Assert.IsTrue(_dapper.IsTableCreated());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _dapper.TeardownDB();
        Assert.IsFalse(_dapper.IsTableCreated());
    }

    [TestMethod()]
    public void ConfigTest()
    {
        Assert.AreEqual("TestTable", _config["TableName"]);
    }

    // CREATE TESTS

    [TestMethod()]
    public void InsertTest()
    {
        Assert.AreEqual(0, GetRecordCount());

        Assert.AreEqual(1, _dapper.Insert(TestSessionA()));
        Assert.AreEqual(1, GetRecordCount());

        Assert.AreEqual(1, _dapper.Insert(TestSessionB()));
        Assert.AreEqual(1, _dapper.Insert(TestSessionC()));
        Assert.AreEqual(3, GetRecordCount());
    }

    // RETRIEVE TESTS

    [TestMethod()]
    public void TryGetSessionTest()
    {
        Assert.AreEqual(0, GetRecordCount());

        _dapper.Insert(TestSessionA());
        _dapper.Insert(TestSessionB());
        _dapper.Insert(TestSessionC());
        Assert.AreEqual(3, GetRecordCount());

        Assert.IsTrue(_dapper.TryGetSession(2, out CodingSession session));
        Assert.IsNotNull(session);
        Assert.AreEqual(2, session.Id);
        Assert.AreEqual("2024-08-15 10:15", session.StartTime);
        Assert.AreEqual("2024-08-15 14:22", session.EndTime);
        Assert.AreEqual(247, session.Duration);
    }

    [TestMethod()]
    public void TryGetSessionOutOfRangeTest()
    {
        Assert.AreEqual(0, GetRecordCount());

        _dapper.Insert(TestSessionA());
        _dapper.Insert(TestSessionB());
        _dapper.Insert(TestSessionC());
        Assert.AreEqual(3, GetRecordCount());

        Assert.IsFalse(_dapper.TryGetSession(-1, out CodingSession session));
        Assert.IsNull(session);
        Assert.IsFalse(_dapper.TryGetSession(0, out session));
        Assert.IsNull(session);
        Assert.IsFalse(_dapper.TryGetSession(4, out session));
        Assert.IsNull(session);
    }

    [TestMethod()]
    public void GetAllSessionsTest()
    {
        CodingSession[] expected = [TestSessionA(), TestSessionB(), TestSessionC()];
        _dapper.Insert(expected[0]);
        _dapper.Insert(expected[1]);
        _dapper.Insert(expected[2]);

        List<CodingSession> sessions = _dapper.GetAllSessions();
        Assert.AreEqual(3, sessions.Count);

        for (int i = 0; i < sessions.Count; i++)
        {
            Assert.AreEqual(i + 1, sessions[i].Id);
            Assert.AreEqual(expected[i].StartTime, sessions[i].StartTime);
            Assert.AreEqual(expected[i].EndTime, sessions[i].EndTime);
            Assert.AreEqual(expected[i].Duration, sessions[i].Duration);
        }
    }

    [TestMethod()]
    public void GetSessionsByYearTest()
    {
        _dapper.Insert(new CodingSession("2021-01-01 10:00", "2021-01-01 12:00"));
        _dapper.Insert(new CodingSession("2022-01-01 10:00", "2022-01-01 12:00"));
        _dapper.Insert(new CodingSession("2022-02-02 10:00", "2022-02-02 12:00"));

        Assert.AreEqual(0, _dapper.GetSessionsByYear("2020").Count);
        Assert.AreEqual(1, _dapper.GetSessionsByYear("2021").Count);
        Assert.AreEqual(2, _dapper.GetSessionsByYear("2022").Count);
    }

    [TestMethod()]
    public void GetSessionsByMonthTest()
    {
        _dapper.Insert(new CodingSession("2022-01-01 10:00", "2021-01-01 12:00"));
        _dapper.Insert(new CodingSession("2022-02-01 10:00", "2022-02-01 12:00"));
        _dapper.Insert(new CodingSession("2022-02-02 10:00", "2022-02-02 12:00"));

        Assert.AreEqual(0, _dapper.GetSessionsByMonth("2022-03").Count);
        Assert.AreEqual(1, _dapper.GetSessionsByMonth("2022-01").Count);
        Assert.AreEqual(2, _dapper.GetSessionsByMonth("2022-02").Count);
    }

    [TestMethod()]
    public void GetSessionByDurationTest()
    {
        _dapper.Insert(new CodingSession("2022-01-01 10:00", "2022-01-01 11:00"));
        _dapper.Insert(new CodingSession("2022-02-01 10:00", "2022-02-01 12:00"));
        _dapper.Insert(new CodingSession("2022-02-02 10:00", "2022-02-02 13:00"));

        Assert.AreEqual(0, _dapper.GetSessionsByDuration(0, 59).Count);
        Assert.AreEqual(1, _dapper.GetSessionsByDuration(0, 90).Count);
        Assert.AreEqual(2, _dapper.GetSessionsByDuration(60, 120).Count);
        Assert.AreEqual(3, _dapper.GetSessionsByDuration(0, 1000000).Count);
    }

    // UPDATE TESTS

    [TestMethod()]
    public void UpdateSessionTest()
    {
        CodingSession[] expected = [TestSessionA(), TestSessionB(), TestSessionC()];
        _dapper.Insert(expected[0]);
        _dapper.Insert(expected[1]);
        _dapper.Insert(expected[2]);

        if (!_dapper.TryGetSession(2, out CodingSession session))
        {
            Assert.Fail("Test session not found in database.");
        }

        Assert.AreEqual(2, session.Id);
        Assert.AreEqual(expected[1].StartTime, session.StartTime);
        Assert.AreEqual(expected[1].EndTime, session.EndTime);
        Assert.AreEqual(expected[1].Duration, session.Duration);

        session.StartTime = "2024-01-01 01:00";
        session.EndTime = "2024-01-01 02:00";

        Assert.IsTrue(_dapper.UpdateSession(session));

        if (!_dapper.TryGetSession(2, out CodingSession updated)) {
            Assert.Fail("Updated test session not found in database.");
        }

        Assert.AreEqual("2024-01-01 01:00", updated.StartTime);
        Assert.AreEqual("2024-01-01 02:00", updated.EndTime);
        Assert.AreEqual(60, updated.Duration);
    }

    [TestMethod()]
    public void UpdateSessionOutOfRangeTest()
    {
        var session = TestSessionA();
        _dapper.Insert(session);

        session.Id = 7;
        session.StartTime = "2024-01-01 01:00";
        session.EndTime = "2024-01-01 02:00";

        Assert.IsFalse(_dapper.UpdateSession(session));

        if (_dapper.TryGetSession(1, out CodingSession updated)) {
            Assert.AreEqual(1, updated.Id);
            Assert.AreEqual("2024-08-14 01:00", updated.StartTime);
            Assert.AreEqual("2024-08-14 02:05", updated.EndTime);
            Assert.AreEqual(65, updated.Duration);
        }
    }

    // DELETE TESTS

    [TestMethod()]
    public void DeleteSessionTest()
    {
        var session = TestSessionA();
        _dapper.Insert(session);
        Assert.IsTrue(_dapper.TryGetSession(1, out var _));

        Assert.IsFalse(_dapper.DeleteSessionById(2));
        Assert.IsTrue(_dapper.TryGetSession(1, out var _));

        Assert.IsTrue(_dapper.DeleteSessionById(1));
        Assert.IsFalse(_dapper.TryGetSession(1, out _));
    }

    [TestMethod()]
    public void DeleteAllSessionsTest()
    {
        for (int i = 0; i < 5; i++)
        {
            _dapper.Insert(TestSessionA());
            _dapper.Insert(TestSessionB());
            _dapper.Insert(TestSessionC());
        }

        Assert.AreEqual(15, GetRecordCount());
        Assert.IsTrue(_dapper.DeleteAllSessions());
        Assert.AreEqual(0, GetRecordCount());
    }
}