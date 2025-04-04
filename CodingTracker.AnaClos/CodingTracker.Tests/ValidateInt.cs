using AnaClos.CodingTracker;
namespace CodingTracker.Tests
{
    [TestClass]
    public class ValidateInt
    {
        Validation validation = new Validation();
        [TestMethod]
        public void ValidateInt_IsTrueWhenStringInputIs_r()
        {
            string input = "r";
            bool result = validation.ValidateInt(input);
            Assert.IsTrue(result,$"{input} is Valid.");
        }

        [TestMethod]
        public void ValidateInt_IsTrueWhenStringInputIs_Int()
        {
            string input = "10";
            bool result = validation.ValidateInt(input);
            Assert.IsTrue(result, $"{input} is Valid.");
        }

        [TestMethod]
        public void ValidateInt_IsFalseWhenStringInputIs_Float()
        {
            string input = "3.14";
            bool result = validation.ValidateInt(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }

        [TestMethod]
        public void ValidateInt_IsFalseWhenStringInputIs_String_Not_r()
        {
            string input = "abc";
            bool result = validation.ValidateInt(input);
            Assert.IsFalse(result, $"{input} is not Valid.");
        }
    }
}