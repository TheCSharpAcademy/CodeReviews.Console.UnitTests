using AnaClos.CodingTracker;
namespace CodingTracker.Tests
{
    [TestClass]
    public class ValidateDate
    {
        Validation validation = new Validation();
        [TestMethod]
        public void ValidateDate_IsTrueWhenStringInputIs_r()
        {
            string input = "r";
            bool result = validation.ValidateDate(input);
            Assert.IsTrue(result,$"{input} is Valid.");
        }

        [TestMethod]
        //Valid Format dd/MM/yy HH:mm:ss
        public void ValidateDate_IsTrueWhenStringInputIs_ValidDateFormat() 
        {
            string input = "29/03/25 22:27:00";
            bool result = validation.ValidateDate(input);
            Assert.IsTrue(result, $"{input} is Valid.");
        }

        [TestMethod]
        public void ValidateDate_IsFalseWhenStringInputIs_InvalidDateFormat()
        {
            string input = "32/03/25 22:27:00";
            bool result = validation.ValidateDate(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }

        [TestMethod]
        public void ValidateDate_IsFalseWhenStringInputIs_Int()
        {
            string input = "20";
            var validation = new Validation();
            bool result = validation.ValidateDate(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }

        [TestMethod]
        public void ValidateDate_IsFalseWhenStringInputIs_Float()
        {
            string input = "100.0";
            bool result = validation.ValidateDate(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }

        [TestMethod]
        public void ValidateDate_IsFalseWhenStringInputIs_String_Not_r()
        {
            string input = "R";
            bool result = validation.ValidateDate(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }
    }
}