using CodingTrack;

namespace CodingTracker.Tests
{
    [TestClass]
    public class TestingValidators
    {

        [TestMethod]
        public void Validators_DateIsValid()
        {
            string a = "09/12/2024 00:12:10";
            DateTime b = default;
            bool isValid = CodingTrack.Validators.IsValidDate(a, ref b);

            Assert.IsTrue(isValid);
        }
        [TestMethod]
        public void Validators_DateIsNotValid()
        {
            string a = "09/12/2024 00:12:10asdasdas";
            DateTime b = default;
            bool isValid = CodingTrack.Validators.IsValidDate(a, ref b);

            Assert.IsTrue(!isValid);
        }

        [TestMethod]
        public void Validators_StartTimeIsLater()
        { 
            DateTime date = DateTime.UtcNow;
            DateTime date1 = DateTime.UtcNow.AddDays(1);
            bool isLater = CodingTrack.Validators.IsStartTimeLater(date, date1);
            Assert.IsTrue(!isLater);
        }

    [TestMethod]
        public void Validators_StartTimeIsEarlier()
        {
            DateTime date = DateTime.UtcNow;
            DateTime date1 = DateTime.UtcNow.AddDays(1);
            bool isLater = CodingTrack.Validators.IsStartTimeLater(date1, date);
            Assert.IsTrue(isLater);
        }
    }
}