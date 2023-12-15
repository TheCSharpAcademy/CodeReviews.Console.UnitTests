using HabitLogger;

namespace Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("10-01-21")]
        [InlineData("10-25-21 9")]
        [InlineData("10-13-16")]
        [InlineData("10-19-21")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("25/01/21 16:83")]
        [InlineData("10/01/21 56:00")]
        [InlineData("15.01.21 16:83")]
        [InlineData("10.13.21 2500")]
        [InlineData("39.13.21")]
        [InlineData("10/13/21 25:00")]
        [InlineData("10:30 10-01-21")]
        public void DateTime_GivenInvalidDate_AssertFalse( string dateTimeStr )
        {
            var result = Validation.ValidateDate(dateTimeStr);

            Assert.False(result, $"{dateTimeStr} should be invalid date time.");
        }

        [Theory]
        [InlineData("09-01-2023")]
        [InlineData("25-10-2019")]
        [InlineData("01-01-2022")]
        [InlineData("29-10-2023")]
        public void DateTime_GivenValidDate_AssertTrue( string dateTimeStr )
        {
            var result = Validation.ValidateDate(dateTimeStr);

            Assert.True(result, $"{dateTimeStr} should be valid date time.");
        }
    }
}