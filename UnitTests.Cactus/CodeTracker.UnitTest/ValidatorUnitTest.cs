using ConConfig;

namespace CodeTracker.UnitTest
{
    [TestClass]
    public class ValidatorUnitTest
    {
        [TestMethod]
        public void IsValidDate_DateInFormat_ReturnTrue()
        {
            // Arrange
            string dateStr = "12:12 06-02-2024";
            DateTime date;

            // Act
            bool res = Validator.IsValidDate(dateStr, out date);

            // Assert
            Assert.IsTrue(res);
        }


        [TestMethod]
        public void IsValidDate_DateOutFormat_ReturnFalse()
        {
            // Arrange
            string dateStr = "12-12 06-02-2024";
            DateTime date;

            // Act
            bool res = Validator.IsValidDate(dateStr, out date);

            // Assert
            Assert.IsFalse(res);
        }


        [TestMethod]
        public void IsValidDate_IsNotDate_ReturnFalse()
        {
            // Arrange
            string dateStr = "isnotdate";
            DateTime date;

            // Act
            bool res = Validator.IsValidDate(dateStr, out date);

            // Assert
            Assert.IsFalse(res);
        }
    }
}