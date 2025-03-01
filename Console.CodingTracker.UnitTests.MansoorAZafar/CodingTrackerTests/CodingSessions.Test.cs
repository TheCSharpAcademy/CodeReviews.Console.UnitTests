using CodingTrackerLibrary.Models;
using System.Reflection;

namespace CodingTrackerTests;

[TestClass]
public class CodingSessionsTest
{
    [TestMethod]
    public void Test_CodingSessionsHeaders()
    {
        string[] expectedHeaders = { "Id", "StartDate", "EndDate", "Duration", "Units" };
        for(int i = 0; i < expectedHeaders.Length; ++i)
            Assert.AreEqual(expectedHeaders[i], CodingSession.headers[i]);
    }

    [TestMethod]
    public void Test_CodingSession_EmptyConstructor()
    {
        CodingSession session = new();
        
        
        Assert.IsFalse(IsPropertyInitialized(session, "Id"));
        Assert.IsFalse(IsPropertyInitialized(session, "StartDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "EndDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "Duration"));
        Assert.IsFalse(IsPropertyInitialized(session, "Units"));
    }


    [TestMethod]
    public void Test_CodingSession_OnlyId_1_Constructor()
    {
        const int id = 1;
        CodingSession session = new(id);


        Assert.IsTrue(IsPropertyInitialized(session, "Id"));
        Assert.IsFalse(IsPropertyInitialized(session, "StartDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "EndDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "Duration"));
        Assert.IsFalse(IsPropertyInitialized(session, "Units"));

        Assert.AreEqual(id, session.Id);
    }

    [TestMethod]
    public void Test_CodingSession_OnlyStartDate_1_Constructor()
    {
        string startDate = "2002-02-05";
        CodingSession session = new(startDate);


        Assert.IsFalse(IsPropertyInitialized(session, "Id"));
        Assert.IsTrue(IsPropertyInitialized(session, "StartDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "EndDate"));
        Assert.IsFalse(IsPropertyInitialized(session, "Duration"));
        Assert.IsFalse(IsPropertyInitialized(session, "Units"));

        Assert.AreEqual(startDate, session.StartDate);
    }

    [TestMethod]
    public void Test_CodingSession_3_Constructor_StartDate_EndDate_Duration()
    {
        string startDate = "2002-02-05";
        string endDate = "2025-05-05";
        float duration = 55.02f;
        CodingSession session = new(startDate, endDate, duration);


        Assert.IsFalse(IsPropertyInitialized(session, "Id"));
        Assert.IsTrue(IsPropertyInitialized(session, "StartDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "EndDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "Duration"));
        Assert.IsFalse(IsPropertyInitialized(session, "Units"));

        Assert.AreEqual(startDate, session.StartDate);
        Assert.AreEqual(endDate, session.EndDate);
        Assert.AreEqual(duration, session.Duration);
    }


    [TestMethod]
    public void Test_CodingSession_4_Constructor_Id_StartDate_EndDate_Duration()
    {
        const int id = 105;
        string startDate = "2002-02-05";
        string endDate = "2025-05-05";
        const float duration = 55.02f;
        CodingSession session = new(id, startDate, endDate, duration);


        Assert.IsTrue(IsPropertyInitialized(session, "Id"));
        Assert.IsTrue(IsPropertyInitialized(session, "StartDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "EndDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "Duration"));
        Assert.IsFalse(IsPropertyInitialized(session, "Units"));

        Assert.AreEqual(id, session.Id);
        Assert.AreEqual(startDate, session.StartDate);
        Assert.AreEqual(endDate, session.EndDate);
        Assert.AreEqual(duration, session.Duration);
    }


    [TestMethod]
    public void Test_CodingSession_5_Constructor_Id_StartDate_EndDate_Duration_Units()
    {
        const int id = 105;
        string startDate = "2002-02-05";
        string endDate = "2025-05-05";
        const float duration = 55.02f;
        string units = "bob";
        CodingSession session = new(id, startDate, endDate, duration, units);


        Assert.IsTrue(IsPropertyInitialized(session, "Id"));
        Assert.IsTrue(IsPropertyInitialized(session, "StartDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "EndDate"));
        Assert.IsTrue(IsPropertyInitialized(session, "Duration"));
        Assert.IsTrue(IsPropertyInitialized(session, "Units"));

        Assert.AreEqual(id, session.Id);
        Assert.AreEqual(startDate, session.StartDate);
        Assert.AreEqual(endDate, session.EndDate);
        Assert.AreEqual(duration, session.Duration);
        Assert.AreEqual(units, session.Units);
    }

    [TestMethod]
    public void Test_EqualsOperation_True()
    {
        CodingSession[] a = 
            {
                new(2),
                new("2022"),
                new("test", "test", 2.2f),
                new(1, "lol", "lol", 2.2f),
                new(1, "lol", "lol", 2.2f, "lol")
            };

        CodingSession[] b =
            {
                 new(2),
                 new("2022"),
                 new("test", "test", 2.2f),
                 new(1, "lol", "lol", 2.2f),
                 new(1, "lol", "lol", 2.2f, "lol")
            };

        for (int i = 0; i < a.Length; ++i)
            Assert.AreEqual(a[i], b[i]);
    }

    [TestMethod]
    public void Test_EqualsOperation_False()
    {
        CodingSession[] a =
            {
                new(2),
                new("2022"),
                new("test", "test", 2.2f),
                new(1, "lol", "lol", 2.2f),
                new(1, "lol", "lol", 2.2f, "lol")
            };

        CodingSession[] b =
            {
                 new(12),
                 new("2025"),
                 new("test", "t33st", 1.2f),
                 new(100, "lol", "lol", 2.2f),
                 new(1, "lol", "lo2", 2.2f, "lol4")
            };

        for (int i = 0; i < a.Length; ++i)
            Assert.AreNotEqual(a[i], b[i]);
    }


    private bool IsPropertyInitialized(object obj, string propertyName)
    => obj.GetType()
        .GetProperty
        (
            propertyName, 
            BindingFlags.Public | BindingFlags.Instance
        )?.GetValue(obj) is not null;


}
