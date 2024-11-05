using CodingTracker.Arashi256.Classes;

namespace CodingTracker_UnitTests.Arashi256
{
    [TestClass]
    public class CommonUIUnitTests
    {
        private readonly Validator _validator = new Validator();

        [TestMethod]
        public void TryParseDate_ShouldReturnTrue_WhenDateIsInCorrectFormat()
        {
            // Arrange
            string validDateString = "12-11-24 15:30:00";
            // Act
            bool isValid = _validator.TryParseDate(validDateString, out DateTime result);
            // Assert
            Assert.IsTrue(isValid);
            Assert.AreEqual(new DateTime(2024, 11, 12, 15, 30, 0), result);
        }

        [TestMethod]
        public void TryParseDate_ShouldReturnFalse_WhenDateIsInIncorrectFormat()
        {
            // Arrange
            string invalidDateString = "2024-11-12 15:30:00";
            DateTime expectedDefaultDate = DateTime.MinValue;
            // Act
            bool isValid = _validator.TryParseDate(invalidDateString, out DateTime result);
            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(expectedDefaultDate, result);
        }

        [TestMethod]
        public void TryParseDate_ShouldReturnFalse_WhenDateStringIsEmpty()
        {
            // Arrange
            string emptyDateString = "";
            DateTime expectedDefaultDate = DateTime.MinValue;
            // Act
            bool isValid = _validator.TryParseDate(emptyDateString, out DateTime result);
            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(expectedDefaultDate, result);
        }
    }
}