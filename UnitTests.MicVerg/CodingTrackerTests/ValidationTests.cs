using CodingTracker;

namespace CodingTrackerTests
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [DataRow("18/09/2023 13:01")]
        [DataRow("01/01/2022 00:00")]
        public void CanBeParsedAndValidated_CorrectInput_ReturnsTrue(string input)
        {
            var validation = Validation.ValidateDate(input);
            Assert.IsNotNull(validation);
            Assert.IsTrue(validation);            
        }

        [TestMethod]
        [DataRow("31/12/26 32:00")]
        [DataRow("31/12/26 12:61")]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow("1")]
        public void CannotBeParsedAndValidated_WrongInput_ReturnsFalse(string input)
        {
            var validation = Validation.ValidateDate(input);
            Assert.IsNotNull(validation);
            Assert.IsFalse(validation);
        }
    }
}