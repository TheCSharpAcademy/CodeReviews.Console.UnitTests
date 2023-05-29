namespace CodeTracker.UnitTests
{
    [TestClass]
    public class CodingSessionTests
    {

        [TestMethod]
        public void UserEnterTime_ValidDateTime_ReturnsTrueAndSetsDateTime()
        {           
            string userInput = "5/26/2023 10:30:00 AM";

            using (StringReader sr = new StringReader(userInput))
            {
                Console.SetIn(sr);

                bool result = UserInput.UserEnterTime(out DateTime actualDateTime);

                Assert.IsTrue(result);              
            }
        }

        [TestMethod]
        public void UserEnterTime_InvalidDateTime_ReturnsFalseAndDefaultDateTime()
        {
            string userInput = "23/5/2023 10:30:00 AM";

            using (StringReader sr = new StringReader(userInput))
            {
                Console.SetIn(sr);

                bool result = UserInput.UserEnterTime(out DateTime actualDateTime);

                Assert.IsFalse(result);
            }
        }
    }
}