using Microsoft.Extensions.Configuration;

namespace CodingTracker.Tests;

[TestClass()]
public class SpectreValidationTests
{
    private IConfiguration _config = null!;
    private SpectreValidation _validation = null!;

    [TestInitialize()]
    public void TestInitialize()
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("testsettings.json", true, true)
            .Build();

        _validation = new SpectreValidation(_config);
    }

    [TestMethod()]
    public void TimeTest()
    {
        Assert.IsFalse(_validation.Time("2024-1-31 10:00").Successful);
        Assert.IsTrue(_validation.Time("2024-01-31 10:00").Successful);
        Assert.IsFalse(_validation.Time("2024-00-01 10:00").Successful);
        Assert.IsFalse(_validation.Time("2024-13-01 10:00").Successful);
        Assert.IsFalse(_validation.Time("2023-02-29 10:00").Successful);
        Assert.IsTrue(_validation.Time("2024-02-29 10:00").Successful);
        Assert.IsFalse(_validation.Time("2024-01-00 10:00").Successful);
        Assert.IsFalse(_validation.Time("2024-01-32 10:00").Successful);

        Assert.IsTrue(_validation.Time("2024-01-01 00:00").Successful);
        Assert.IsTrue(_validation.Time("2024-01-01 00:59").Successful);
        Assert.IsFalse(_validation.Time("2024-01-01 24:00").Successful);
        Assert.IsFalse(_validation.Time("2024-01-01 25:00").Successful);
        Assert.IsFalse(_validation.Time("2024-01-01 00:60").Successful);
        Assert.IsFalse(_validation.Time("2024-01-01 00:61").Successful);
    }

    [TestMethod()]
    public void EndTimeTest()
    {
        string startTime = "2024-02-02 10:10";

        Assert.IsFalse(_validation.EndTime(startTime, "2023-02-02 11:00").Successful);
        Assert.IsFalse(_validation.EndTime(startTime, "2024-01-02 11:00").Successful);
        Assert.IsFalse(_validation.EndTime(startTime, "2024-02-01 11:00").Successful);
        Assert.IsFalse(_validation.EndTime(startTime, "2024-02-02 09:00").Successful);
        Assert.IsFalse(_validation.EndTime(startTime, "2024-02-02 10:00").Successful);

        Assert.IsTrue(_validation.EndTime(startTime, "2024-02-02 10:10").Successful);

        Assert.IsTrue(_validation.EndTime(startTime, "2025-02-02 10:10").Successful);
        Assert.IsTrue(_validation.EndTime(startTime, "2024-03-02 10:10").Successful);
        Assert.IsTrue(_validation.EndTime(startTime, "2024-02-03 10:10").Successful);
        Assert.IsTrue(_validation.EndTime(startTime, "2024-02-02 11:10").Successful);
        Assert.IsTrue(_validation.EndTime(startTime, "2024-02-02 10:11").Successful);
    }

    [TestMethod()]
    public void YearTest()
    {
        Assert.IsFalse(_validation.Year("132").Successful);
        Assert.IsFalse(_validation.Year("13245").Successful);

        string future = (DateTime.Now.Year + 1).ToString();
        Assert.IsFalse(_validation.Year(future).Successful);

        string current = DateTime.Now.Year.ToString();
        Assert.IsTrue(_validation.Year(current).Successful);

        string past = (DateTime.Now.Year - 1).ToString();
        Assert.IsTrue(_validation.Year(past).Successful);
    }

    [TestMethod()]
    public void MonthTest()
    {
        Assert.IsTrue(_validation.Month("2024-01").Successful);
        Assert.IsFalse(_validation.Month("2024-1").Successful);
        Assert.IsFalse(_validation.Month("24-01").Successful);
        Assert.IsFalse(_validation.Month("2024-00").Successful);
        Assert.IsFalse(_validation.Month("2024-13").Successful);
    }

    [TestMethod()]
    public void MinDurationTest()
    {
        Assert.IsFalse(_validation.MinDuration(-1).Successful);
        Assert.IsTrue(_validation.MinDuration(0).Successful);
        Assert.IsTrue(_validation.MinDuration(1).Successful);
    }

    [TestMethod()]
    public void MaxDurationTest()
    {
        Assert.IsFalse(_validation.MaxDuration(10, 20).Successful);
        Assert.IsTrue(_validation.MaxDuration(20, 10).Successful);
    }

    [TestMethod()]
    public void PositiveIdTest()
    {
        Assert.IsFalse(_validation.PositiveId(-1).Successful);
        Assert.IsTrue(_validation.PositiveId(0).Successful);
        Assert.IsTrue(_validation.PositiveId(1).Successful);
    }
}