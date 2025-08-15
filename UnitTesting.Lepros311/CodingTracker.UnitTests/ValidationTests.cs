using CodingTracker.Model;
using System.Globalization;
using Moq;

namespace CodingTracker.UnitTests;

[TestClass]
public sealed class ValidationTests
{
    [TestMethod]
    public void ValidateDate_CorrectlyFormattedInputMonthFirstThenDay_ReturnsInputtedDate()
    {
        var validation = new Validation();

        string dateInput = "12/05/2024";

        string format = "MM/dd/yyyy";

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out DateTime date))
        {
            date = date.Date;
        }

        var result = validation.ValidateDate(dateInput);

        Assert.AreEqual(date, result);
    }

    [TestMethod]
    public void ValidateDate_IncorrectlyFormattedInputDayFirstThenMonth_ReturnsNull()
    {
        var validation = new Validation();

        string dateInput = "31/05/2024";

        string format = "MM/dd/yyyy";

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (DateTime.TryParseExact(dateInput, format, provider, DateTimeStyles.None, out DateTime date))
        {
            date = date.Date;
        }

        var result = validation.ValidateDate(dateInput);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void ValidateTime_CorrectlyFormattedInputHHmmtt_ReturnsInputtedTime()
    {
        var validation = new Validation();

        string timeInput = "5:55 pm";

        string format = "h:mm tt";

        CultureInfo provider = CultureInfo.InvariantCulture;
        if (DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime time))
        {
            time = time;
        }

        var result = validation.ValidateTime(timeInput);

        Assert.AreEqual(time, result);
    }

    [TestMethod]
    public void ValidateTime_IncorrectlyFormattedInputNoAmOrPm_ReturnsNull()
    {
        var validation = new Validation();

        string timeInput = "10:55";

        string format = "h:mm tt";

        CultureInfo provider = CultureInfo.InvariantCulture;

        DateTime.TryParseExact(timeInput, format, provider, DateTimeStyles.None, out DateTime time);
 
        var result = validation.ValidateTime(timeInput);

        Assert.IsNull(result);
    }

    [TestMethod]
    public void ValidateStartTimeIsLessThanEndTime_StartTimeIsLessThanEndTime_ReturnsTrue()
    {
        var validation = new Validation();

        string startTimeInput = "10:30 am";
        string endTimeInput = "11:30 am";

        string format = "h:mm tt";

        CultureInfo provider = CultureInfo.InvariantCulture;

        DateTime.TryParseExact(startTimeInput, format, provider, DateTimeStyles.None, out DateTime startTime);

        DateTime.TryParseExact(endTimeInput, format, provider, DateTimeStyles.None, out DateTime endTime);

        var result = validation.ValidateStartTimeIsLessThanEndTime(startTime, endTime);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ValidateStartTimeIsLessThanEndTime_StartTimeIsGreaterThanEndTime_ReturnsFalse()
    {
        var validation = new Validation();

        string startTimeInput = "12:30 pm";
        string endTimeInput = "11:30 am";

        string format = "h:mm tt";

        CultureInfo provider = CultureInfo.InvariantCulture;

        DateTime.TryParseExact(startTimeInput, format, provider, DateTimeStyles.None, out DateTime startTime);

        DateTime.TryParseExact(endTimeInput, format, provider, DateTimeStyles.None, out DateTime endTime);

        var result = validation.ValidateStartTimeIsLessThanEndTime(startTime, endTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateStartTimeIsLessThanEndTime_StartTimeIsEqualToEndTime_ReturnsFalse()
    {
        var validation = new Validation();

        string startTimeInput = "12:30 pm";
        string endTimeInput = "12:30 pm";

        string format = "h:mm tt";

        CultureInfo provider = CultureInfo.InvariantCulture;

        DateTime.TryParseExact(startTimeInput, format, provider, DateTimeStyles.None, out DateTime startTime);

        DateTime.TryParseExact(endTimeInput, format, provider, DateTimeStyles.None, out DateTime endTime);

        var result = validation.ValidateStartTimeIsLessThanEndTime(startTime, endTime);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ValidateRecordId_ValidInputIdFound_ReturnsEmptyStringTrueAndIdInt()
    {
        var mockRepo = new Mock<ICodingSessionRepository>();
        mockRepo.Setup(r => r.GetRecordIdCount(5)).Returns(1);

        var validation = new Validation(mockRepo.Object);

        string recordIdInput = "5";

        var result = validation.ValidateRecordId(recordIdInput);

        Assert.AreEqual("", result.message);
        Assert.AreEqual(true, result.validStatus);
        Assert.AreEqual(5, result.recordId);
    }

    [TestMethod]
    public void ValidateRecordId_ValidInputNoIdFound_ReturnsNotFoundMessageFalseAndIdInt()
    {
        var mockRepo = new Mock<ICodingSessionRepository>();
        mockRepo.Setup(r => r.GetRecordIdCount(123)).Returns(0);

        var validation = new Validation(mockRepo.Object);

        string recordIdInput = "123";

        var result = validation.ValidateRecordId(recordIdInput);

        Assert.AreEqual("Record not found. Please enter a valid record ID.", result.message);
        Assert.AreEqual(false, result.validStatus);
        Assert.AreEqual(123, result.recordId);
    }

    [TestMethod]
    public void ValidateRecordId_InvalidInput_ReturnsInvalidIdMessageFalseAndDefaultInt()
    {
        var mockRepo = new Mock<ICodingSessionRepository>();

        var validation = new Validation(mockRepo.Object);

        string recordIdInput = "w";

        var result = validation.ValidateRecordId(recordIdInput);

        Assert.AreEqual("Invalid ID. Please enter a numeric value.", result.message);
        Assert.AreEqual(false, result.validStatus);
        Assert.AreEqual(0, result.recordId);
    }

    [TestMethod]
    public void ValidateDeleteConfirmation_InputY_ReturnsY()
    {
        var validation = new Validation();

        string confirmationInput = "y";

        var result = validation.ValidateDeleteConfirmation(confirmationInput);

        Assert.AreEqual("y", result);
    }

    [TestMethod]
    public void ValidateDeleteConfirmation_InputN_ReturnsN()
    {
        var validation = new Validation();

        string confirmationInput = "n";

        var result = validation.ValidateDeleteConfirmation(confirmationInput);

        Assert.AreEqual("n", result);
    }

    [TestMethod]
    public void ValidateDeleteConfirmation_InputEmptyString_ReturnsEmptyString()
    {
        var validation = new Validation();

        string confirmationInput = "";

        var result = validation.ValidateDeleteConfirmation(confirmationInput);

        Assert.AreEqual("", result);
    }
}
