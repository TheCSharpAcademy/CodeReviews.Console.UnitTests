using CodingTracker.Database.Helpers;

namespace CodingTracker.Tests;

[TestClass]
public class DateTimeValidatorTests
{
  [TestMethod]
  public void IsDateTimeValid_ValidFormat()
  {
    // Assign
    string date = "2024-04-23 08:00";
    // Act
    bool isValid = DateTimeValidator.IsValid(date);
    // Assert
    Assert.IsTrue(isValid);
  }

  [TestMethod]
  public void IsDateTimeValid_InvalidFormat()
  {
    // Assign
    string date = "23-04-2024 08:00";
    // Act
    bool isValid = DateTimeValidator.IsValid(date);
    // Assert
    Assert.IsFalse(isValid);
  }

  [TestMethod]
  public void IsDateTimeValid_Null()
  {
    // Assign
    string? date = null;
    // Assert // Act
    Assert.ThrowsException<ArgumentNullException>(() => DateTimeValidator.IsValid(date));
  }

  [TestMethod]
  public void IsDateTimeValid_Before2000()
  {
    // Assign 
    string date = "1999-12-31 02:22";
    // Act
    bool isValid = DateTimeValidator.IsValid(date);
    // Assert
    Assert.IsFalse(isValid);
  }

  [TestMethod]
  public void AreDateTimesValid_EndDateBeforeStartDate()
  {
    // Assign
    string startDate = "2022-05-23 05:00";
    string endDate = "2022-04-22 06:00";
    // Act
    bool areValid = DateTimeValidator.AreValid(startDate, endDate);
    // Assert
    Assert.IsFalse(areValid);
  }

  [TestMethod]
  public void AreDateTimesValid_Valid()
  {
    // Assign
    string startDate = "2022-04-22 06:00";
    string endDate = "2022-05-23 05:00";
    // Act
    bool areValid = DateTimeValidator.AreValid(startDate, endDate);
    // Assert
    Assert.IsTrue(areValid);
  }

  [TestMethod]
  public void IsDateTimeInThePast_Valid()
  {
    // Assign
    string date = "2023-04-24 05:00";
    // Act
    bool isValid = DateTimeValidator.IsInThePast(date);
    // Assert
    Assert.IsTrue(isValid);
  }

  [TestMethod]
  public void IsDateTimeInThePast_Invalid()
  {
    // Assign
    string date = "2025-04-24 05:00";
    // Act
    bool isValid = DateTimeValidator.IsInThePast(date);
    // Assert
    Assert.IsFalse(isValid);
  }
}