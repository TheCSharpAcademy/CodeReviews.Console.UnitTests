using CodingTrackerJMS;

namespace UnitTest;

[TestClass]
public class UnitTest1
{
    Validation validation = new Validation();

    [TestMethod]
    [DataRow("2022/11/05; 10:00", false, null)]
    public void GetValidDate_ValidDates_ReturnsTrue(string value1, bool value2, DateTime value3)
    {
        bool result = validation.GetValidDate(value1, value2, out value3);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("11/11/2023; 10:00", false, null)]
    [DataRow("11:11:2023; 10:00", false, null)]
    [DataRow("11/11/2023", false, null)]
    [DataRow("abcde", false, null)]

    public void GetValidDate_InvalidDates_ReturnsFalse(string value1, bool value2, DateTime value3)
    {
        bool result = validation.GetValidDate(value1, value2, out value3);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("1")]
    [DataRow("123")]

    public void GetValidId_ValidId_ReturnsTrue(string value1)
    {
        int id;
        bool result;

        (id, result) = validation.GetValidID(value1);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("c")]
    [DataRow(null)]
    public void GetValidId_InvalidId_ReturnsFalse(string value2)
    {
        int id;
        bool result;

        (id, result) = validation.GetValidID(value2);

        Assert.IsFalse(result);
    }
}