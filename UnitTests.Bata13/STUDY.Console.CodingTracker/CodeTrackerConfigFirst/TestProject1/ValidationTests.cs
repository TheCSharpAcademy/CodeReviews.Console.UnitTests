using CodeTrackerConfigFirst;

namespace TestProject1
{
    [TestClass]
    public class ValidationTests
    {
        [TestMethod]
        [DataRow("1")]
        [DataRow("53")]
        [DataRow("9999999")]
        public void IsValidInt_ForPositiveInteger_ReturnTrue(string numberInput)
        {
            //act
            bool PassedIsValidIntTest = Validation.IsValidInt(numberInput);

            //assert
            Assert.AreEqual(true, PassedIsValidIntTest);
        }

        [TestMethod]
        [DataRow("-3")]
        public void IsNonNegativeNumber_ForNegativeNumber_ReturnFalse(string numberInput)
        {
            // Act 
            bool PassedIsNonNegativeNumber = Validation.IsNonNegativeNumber(numberInput);

            //Assert
            Assert.AreEqual(false, PassedIsNonNegativeNumber);
        }
               
        [TestMethod]
        [DataRow("0")]
        public void IsNot0_ForUserInput0_ReturnFalse(string numberInput)
        {
            // Act 
            bool PassedIsNot0Test = Validation.IsNot0(numberInput);

            //Assert
            Assert.AreEqual(false, PassedIsNot0Test);
        }

        [TestMethod]
        [DataRow("Seven")]
        public void IsNonStringValue_ForStringInput_ReturnFalse(string numberInput)
        {
            // Act
            bool PassedIsNonStringValue = Validation.IsNonStringValue(numberInput);

            // Assert
            Assert.AreEqual(false, PassedIsNonStringValue);
        }
    }
}