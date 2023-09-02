using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingTracker.UnitTests.Services
{
    [TestClass]
    public class ValidationService
    {
        [TestMethod]
        [DataRow("1/09/2023 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("1/9/2023 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("1/9/23 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("01/9/2023 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("1/09/2023 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("01/09/2023 12:00", "dd/mm/yyyy HH:mm")]
        public void IsDateValid__IfNot__ReturnFalse(string date, string format)
        {
            bool result = Validation.ValidateDate(date, format);

            Assert.IsFalse(result, "Invalid inputs");
        }

        [TestMethod]
        [DataRow("01/09/2023 12:00", "dd/MM/yyyy HH:mm")]
        [DataRow("01/01/2023 00:00", "dd/MM/yyyy HH:mm")]
        public void IsDateValid__IfYes__ReturnTrue(string date, string format)
        {
            bool result = Validation.ValidateDate(date, format);

            Assert.IsTrue(result, "Valid");
        }

        [TestMethod]
        [DataRow("1.5")]
        [DataRow("-1")]
        [DataRow("-1.5")]
        [DataRow("1a")]
        [DataRow("a1")]
        [DataRow("a")]
        public void IsNumberValid__IfNot__ReturnFalse(string value)
        {
            bool result = Validation.ValidateNumber(value);

            Assert.IsFalse(result, "Invalid number");
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("1")]
        public void IsNumberValid__IfYes__ReturnTrue(string value)
        {
            bool result = Validation.ValidateNumber(value);

            Assert.IsTrue(result, "Valid");
        }

        [TestMethod]
        [DataRow("01/09/2023 12:00", "01/09/2023 11:00", "dd/MM/yyyy HH:mm")]
        public void IsDurationValid__IfNot__ReturnFalse(string startTime, string endTime, string format)
        {
            bool result = Validation.ValidateDuration(startTime, endTime, format);

            Assert.IsFalse(result, "Invalid duration");
        }

        [TestMethod]
        [DataRow("10/12/2022 12:00", "10/12/2022 16:00", "dd/MM/yyyy HH:mm")]
        public void IsDurationValid__IfYes__ReturnTrue(string startTime, string endTime, string format)
        {
            bool result = Validation.ValidateDuration(startTime, endTime, format);

            Assert.IsTrue(result, "Valid");
        }
    }
}