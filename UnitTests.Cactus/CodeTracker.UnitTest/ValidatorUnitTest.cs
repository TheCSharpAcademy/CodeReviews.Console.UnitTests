using ConConfig;

namespace CodeTracker.UnitTest
{
    [TestClass]
    public class ValidatorUnitTest
    {
        [TestMethod]
        [DataRow("12:12 06-02-2024")]
        [DataRow("02:02 06-02-2024")]
        public void IsValidDate_FormattedDate_ReturnTrue(string dateStr)
        {
            // Act
            bool res = Validator.IsValidDate(dateStr, out _);

            // Assert
            Assert.IsTrue(res);
        }


        [TestMethod]
        [DataRow("2:12 06-02-2024")]
        [DataRow("12:2 06-02-2024")]
        [DataRow("12:12 6-2-2024")]
        [DataRow("12-12 6-2-2024")]
        public void IsValidDate_NonFormatttedDate_ReturnFalse(string dateStr)
        {
            // Act
            bool res = Validator.IsValidDate(dateStr, out _);

            // Assert
            Assert.IsFalse(res);
        }


        [TestMethod]
        [DataRow("1")]
        [DataRow("nonDateStr")]
        public void IsValidDate_IsNotDate_ReturnFalse(string nonDateStr)
        {
            // Act
            bool res = Validator.IsValidDate(nonDateStr, out _);

            // Assert
            Assert.IsFalse(res);
        }
    }
}