using AnaClos.CodingTracker;
namespace CodingTracker.Tests
{
    [TestClass]
    public class Validate2Dates
    {
        Validation validation = new Validation();
        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_IsTrueWhenInputsAreValidDatesAndInput1BeforeInput2()
        {
            string input1 = "29/03/25 22:27:00";
            string input2 = "30/03/25 23:59:59";
            bool result = validation.Validate2Dates(input1,input2);
            Assert.IsTrue(result,$"{input1} {input2} are Valid.");
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_IsFalseWhenInputsAreValidDatesAndInput1AfterInput2() 
        {
            string input1 = "30/03/25 23:59:59";
            string input2 = "29/03/25 22:27:00";
            bool result = validation.Validate2Dates(input1, input2);
            Assert.IsFalse(result, $"{input1} {input2} are not Valid.");
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_IsFalseWhenInputsAreValidDatesAndInput1EqualInput2()
        {
            string input1 = "29/03/25 22:27:00";
            string input2 = "29/03/25 22:27:00";
            bool result = validation.Validate2Dates(input1, input2);
            Assert.IsFalse(result, $"{input1} {input2} are not Valid.");
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_ThrowExceptionWhenInput1IsValidAndInput2IsStringNotValid()
        {
            string input1 = "29/03/25 22:27:00";
            string input2 = "Some text";
            bool notValid = true;
            try
            {
                bool result = validation.Validate2Dates(input1, input2);
                Assert.IsFalse(result, $"{input1} {input2} are not Valid.");
            }
            catch(FormatException ex)
            {
                Assert.IsTrue(notValid);
            }            
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_ThrowExceptionWhenInput2IsValidAndInput1IsStringNotValid()
        {
            string input1 = "Not valid";
            string input2 = "29/03/25 22:27:00";
            bool notValid = true;
            try
            {
                bool result = validation.Validate2Dates(input1, input2);
                Assert.IsFalse(result, $"{input1} {input2} are not Valid.");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(notValid);
            }
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void Validate2Dates_ThrowExceptionWhenInputsAreStringsNotValid()
        {
            string input1 = "10.45";
            string input2 = "120";
            bool notValid = true;
            try
            {
                bool result = validation.Validate2Dates(input1, input2);
                Assert.IsFalse(result, $"{input1} {input2} are not Valid.");
            }
            catch (FormatException ex)
            {
                Assert.IsTrue(notValid);
            }
        }
    }
}