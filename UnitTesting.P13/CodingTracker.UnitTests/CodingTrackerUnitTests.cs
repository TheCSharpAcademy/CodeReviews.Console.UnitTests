using CodingTracker.Controllers;

namespace CodingTracker.UnitTests;

[TestClass]
public sealed class CodingTrackerUnitTests
{
    [TestMethod]
    [DataRow("Mon Copain de classe est un singe")]
    [DataRow("Pokedex")]
    [DataRow("Couscous Manager")]
    [DataRow("Personal Project")]
    public void ValidateString_ValidInput_ReturnsTrue(string str)
    {
        bool result = Validators.ValidateString(str);
        Assert.IsTrue(result, $"{str} should be true");
    }

    [TestMethod]
    [DataRow("(Select * From)")]
    [DataRow("reveil-matin")]
    [DataRow("--Comment")]
    [DataRow("/*Comment*/")]
    public void ValidateString_InavlidInput_ReturnsFalse(string str)
    {
        bool result = Validators.ValidateString(str);
        Assert.IsFalse(result, $"{str} should be false");
    }

    [TestMethod]
    [DataRow("2024.01.02")]
    [DataRow("2024.03.09")]
    [DataRow("2024.05.22")]
    [DataRow("2024.11.13")]
    public void ValidateDate_ValidInput_ReturnsTrue(string str)
    {
        bool result = Validators.ValidateDate(str);
        Assert.IsTrue(result, $"{str} should be true");
    }

    [TestMethod]
    [DataRow("2024.01-02")]
    [DataRow("2024:10:10")]
    [DataRow("2024.15.01")]
    [DataRow("2024.11.33")]
    public void ValidateDate_InvalidInput_ReturnsFalse(string str)
    {
        bool result = Validators.ValidateDate(str);
        Assert.IsFalse(result, $"{str} should be false");
    }

    [TestMethod]
    [DataRow("12:12")]
    [DataRow("10:45")]
    [DataRow("22:53")]
    [DataRow("00:17")]
    public void ValidateTime_ValidInput_ReturnsTrue(string str)
    {
        bool result = Validators.ValidateTime(str);
        Assert.IsTrue(result, $"{str} should be true");
    }

    [TestMethod]
    [DataRow("22:65")]
    [DataRow("10-10")]
    [DataRow("12.12")]
    [DataRow("32:15")]
    public void ValidateTime_InvalidInput_ReturnsFalse(string str)
    {
        bool result = Validators.ValidateTime(str);
        Assert.IsFalse(result, $"{str} should be false");
    }

    [TestMethod]
    [DataRow("2024-01-02 10:10","2024-01-02 12:12")]
    [DataRow("2024-03-09 10:10", "2024-03-09 13:12")]
    [DataRow("2024-05-22 10:10", "2024-05-22 15:12")]
    [DataRow("2024-11-13 10:10", "2024-11-13 21:12")]
    public void ValidateStartAndEnd_ValidInput_ReturnsTrue(string start,string end)
    {
        bool result = Validators.ValidateStartAndEnd(start, end);
        Assert.IsTrue(result, $"ValidateStartAndEnd_ValidInput_ReturnsTrue {start} // {end} should be true");
    }

    [TestMethod]
    [DataRow("2024-01-02 10:10", "2024-01-02 09:12")]
    [DataRow("2024-03-09 10:10", "2024-03-09 10:10")]
    [DataRow("2024-05-22 10:10", "2024-05-21 15:12")]
    [DataRow("2024-12-13 10:10", "2024-11-13 21:12")]
    public void ValidateStartAndEnd_InvalidInput_ReturnsFalse(string start, string end)
    {
        bool result = Validators.ValidateStartAndEnd(start, end);
        Assert.IsFalse(result, $"ValidateStartAndEnd_ValidInput_ReturnsTrue {start} // {end} should be false");
    }
}
