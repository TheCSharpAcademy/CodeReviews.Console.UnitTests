using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CodingTracker.ConsoleInteraction;
using CodingTracker.Utilities;

namespace CodingTracker.Tests;

[TestClass]
    public class UtilsTests
    {
        private Mock<IUserInteraction> _mockUserInteraction = null!;
        private Utils _utils = null!;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserInteraction = new Mock<IUserInteraction>();
            _utils = new Utils(_mockUserInteraction.Object);
        }

        [TestMethod]
        public void ValidatedStartTime_ValidInput_ReturnsCorrectDateTime()
        {
            // Arrange
            _mockUserInteraction.SetupSequence(ui => ui.GetUserInput())
                .Returns("n")
                .Returns("01-01-22 12:00:00");

            // Act
            DateTime result = _utils.ValidatedStartTime();

            // Assert
            Assert.AreEqual(new DateTime(2022, 1, 1, 12, 0, 0), result);
        }

        [TestMethod]
        public void ValidatedStartTime_InvalidInputThenValid_ReturnsCorrectDateTime()
        {
            // Arrange
            _mockUserInteraction.SetupSequence(ui => ui.GetUserInput())
                .Returns("n")
                .Returns("invalid date")
                .Returns("01-01-22 12:00:00");

            // Act
            DateTime result = _utils.ValidatedStartTime();

            // Assert
            Assert.AreEqual(new DateTime(2022, 1, 1, 12, 0, 0), result);
            _mockUserInteraction.Verify(ui => ui.ShowMessageTimeout("[Red]Please enter a valid date.[/]"), Times.Once);
        }

        [TestMethod]
        public void ValidatedEndTime_ValidInput_ReturnsCorrectDateTime()
        {
            // Arrange
            _mockUserInteraction.Setup(ui => ui.GetUserInput()).Returns("01-01-22 14:30:00");

            // Act
            DateTime result = _utils.ValidatedEndTime();

            // Assert
            Assert.AreEqual(new DateTime(2022, 1, 1, 14, 30, 0), result);
        }

        [TestMethod]
        public void ValidatedEndTime_InvalidInputThenValid_ReturnsCorrectDateTime()
        {
            // Arrange
            _mockUserInteraction.SetupSequence(ui => ui.GetUserInput())
                .Returns("invalid date")
                .Returns("01-01-22 14:30:00");

            // Act
            DateTime result = _utils.ValidatedEndTime();

            // Assert
            Assert.AreEqual(new DateTime(2022, 1, 1, 14, 30, 0), result);
            _mockUserInteraction.Verify(ui => ui.ShowMessageTimeout("[Red]Please enter a valid date.[/]"), Times.Once);
        }

        [TestMethod]
        public void ValidatedEndTime_EndTimeBeforeStartTime_ReturnsTrue()
        {
            // Arrange
            DateTime startTime = new DateTime(2022, 1, 1, 12, 0, 0);
            DateTime endTime = new DateTime(2022, 1, 1, 11, 0, 0);

            // Act
            bool result = _utils.ValidatedEndTime(startTime, endTime);

            // Assert
            Assert.IsTrue(result);
            _mockUserInteraction.Verify(ui => ui.ShowMessageTimeout("\n[Red]The End Time must be after the Start Time[/]\n"), Times.Once);
        }

        [TestMethod]
        public void ValidatedEndTime_EndTimeAfterStartTime_ReturnsFalse()
        {
            // Arrange
            DateTime startTime = new DateTime(2022, 1, 1, 12, 0, 0);
            DateTime endTime = new DateTime(2022, 1, 1, 13, 0, 0);

            // Act
            bool result = _utils.ValidatedEndTime(startTime, endTime);

            // Assert
            Assert.IsFalse(result);
        }
    }