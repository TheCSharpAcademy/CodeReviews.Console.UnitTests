using CodingTrackerJMS;

namespace UnitTest;

[TestClass]
public class UnitTest1
{
    Validation validation = new Validation();

    [TestMethod]
    [DataRow("2022/11/05; 10:00", null)]
    public void GetValidDate_ValidDates_ReturnsTrue(string value1, DateTime value2)
    {
        bool result = validation.GetValidDate(value1, out value2);

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2023/500/11; 10:00", null)]
    [DataRow("2023/11/11; 85:00", null)]
    [DataRow("11/11/2023; 10:00", null)]
    [DataRow("11:11:2023; 10:00", null)]
    [DataRow("11/11/2023", null)]
    [DataRow("abcde", null)]

    public void GetValidDate_InvalidDates_ReturnsFalse(string value1, DateTime value2)
    {
        bool result = validation.GetValidDate(value1, out value2);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow("2023/11/11; 20:00", "2023/11/11; 21:00")]
    public void isTotalTimeValid_ValidTotalTime_ReturnsTrue(string startDateS, string endDateS)
    {
        DateTime startDate, endDate;
        bool result;

        bool startDateValid = validation.GetValidDate(startDateS , out startDate);
        bool endDateValid = validation.GetValidDate(endDateS, out endDate);

        if (startDateValid == true && endDateValid == true)
        {
            result = validation.isTotalTimeValid(startDate, endDate);
        }
        else
        {
            result = false;
        }

        Assert.IsTrue(result);
    }

    [TestMethod]
    [DataRow("2023/11/11; 20:00", "2023/11/11; 19:00")]
    [DataRow("asda", "2023/11/11; 19:00")]

    public void isTotalTimeValid_invalidTotalTime_ReturnsTrue(string startDateS, string endDateS)
    {
        DateTime startDate, endDate;
        bool result;

        bool startDateValid = validation.GetValidDate(startDateS, out startDate);
        bool endDateValid = validation.GetValidDate(endDateS, out endDate);

        if (startDateValid == true && endDateValid == true)
        {
            result = validation.isTotalTimeValid(startDate, endDate);
        }
        else
        {
            result = false;
        }

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