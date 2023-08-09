using CodingTracker.kmakai.Controllers;

namespace CodingTrackerUnitTests.kmakai
{
    [TestClass]
    public class InpuControllerTests
    {
        // IsValid date tests
        [TestMethod]
        [DataRow("01")]
        [DataRow("2021-13-01")]
        [DataRow("2021-12-65")]
        [DataRow(" ")]
        public void IsDateValid_InValidDates_ReturnsFalse(string value)
        {
            var result = InputController.IsDateValid(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("2021-01-01")]
        [DataRow("2021-12-31")]
        [DataRow("01-12-2021")]
        public void IsDateValid_ValidDates_ReturnsTrue(string value)
        {
            var result = InputController.IsDateValid(value);

            Assert.IsTrue(result);
        }


        // IsValid time tests

        [TestMethod]
        [DataRow("01:80")]
        [DataRow("25:00")]
        [DataRow(" ")]
         public void IsTimeValid_InValidTimes_ReturnsFalse(string value)
        {
            var result = InputController.IsTimeValid(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("01:01")]
        [DataRow("23:59")]
        [DataRow("01:00")]
        public void IsTimeValid_ValidTimes_ReturnsTrue(string value)
        {
            var result = InputController.IsTimeValid(value);

            Assert.IsTrue(result);
        }
    }
}