using CodeTrackerConfigFirst;

namespace TestProject1
{
    [TestClass]    
    public class ValidationTests
    {      
        [TestMethod]
        [DataRow(1)]
        [DataRow(53)]
        [DataRow(9999999)]
        public void ValidationTest_ForPositiveNumber_ReturnSuccess(int positiveNumberInput)
        {
            // Arrange
            string message = "\n\nPlease type Id of the record you would like to update. Type 0 to go back to Main Menu.\n\n";

            string userInput = positiveNumberInput.ToString() + "\n";
            StringReader stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);

            // Act
            int finalInput = Validation.GetNumberInput(message);

            // Assert
            Assert.AreEqual(positiveNumberInput, finalInput);
        }

        [TestMethod]
        [DataRow(-3)]
        public void ValidationTest_ForNegativeNumber_ReturnError(int negativeNumberInput)
        {
            // Arrange
            string message = "\n\nPlease type Id of the record you would like to update. Type 0 to go back to Main Menu.\n\n";

            string userInput = negativeNumberInput.ToString() + "\n";
            StringReader stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);

            // Act
            int finalInput = Validation.GetNumberInput(message);

            // Assert
            Assert.AreEqual(negativeNumberInput, finalInput);
        }

        [TestMethod]
        [DataRow(0)]
        public void ValidationTest_ForUserInput0_ReturnError(int zero)
        {
            // Arrange
            string message = "\n\nPlease type Id of the record you would like to update. Type 0 to go back to Main Menu.\n\n";

            string userInput = zero.ToString() + "\n";
            StringReader stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);

            // Act
            int finalInput = Validation.GetNumberInput(message);

            // Assert
            Assert.AreEqual(zero, finalInput);
        }

        [TestMethod]
        [DataRow("Seven")]
        public void ValidationTest_ForStringInput_ReturnError(string stringInput)
        {
            // Arrange
            string message = "\n\nPlease type Id of the record you would like to update. Type 0 to go back to Main Menu.\n\n";

            string userInput = stringInput.ToString() + "\n";
            StringReader stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);

            // Act
            int finalInput = Validation.GetNumberInput(message);

            // Assert
            Assert.IsTrue(finalInput.ToString() == stringInput);
        }      
    }
}