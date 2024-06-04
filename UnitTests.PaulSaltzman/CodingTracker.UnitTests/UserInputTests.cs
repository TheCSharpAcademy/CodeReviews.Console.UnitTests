using System;
using Xunit;
using CodingTracker.Paul_W_Saltzman;
using Xunit.Sdk;

namespace CodingTracker.UnitTests
{
    public class UserInputTests
    {
        [Theory]
        [InlineData("12-25-2023", true)] 
        [InlineData("01-01-1900", true)] 
        [InlineData("02-29-2024", true)]
        [InlineData("13-01-2023", false)] 
        [InlineData("12-32-2023", false)] 
        [InlineData("invalid", false)] 
        [InlineData("", false)] 

        public void CanParseDate_ReturnsBool(string input, bool canParse)
        {

            // Act
            var result = UserInput.CanParseDate(input);

            // Assert
            Assert.Equal(canParse, result);
        }

        [Theory]
        [InlineData("12-25-2023", 2023, 12, 25)] 
        [InlineData("01-01-1900", 1900, 01, 01)] 
        [InlineData("02-29-2024", 2024, 02, 29)] 
        [InlineData("13-01-2023", 1900, 01, 01)] 
        [InlineData("12-32-2023", 1900, 01, 01)] 
        [InlineData("invalid", 1900, 01, 01)] 
        [InlineData("", 1900, 01, 01)] 
        public void ParseDate_ReturnsDateOnly(string input, int expectedYear, int expectedMonth, int expectedDay)
        {
            // Arrange
            var expectedDate = new DateOnly(expectedYear, expectedMonth, expectedDay);

            // Act
            var result = UserInput.ParseDate(input);

            // Assert
            Assert.Equal(expectedDate, result);
        }

        [Theory]
        [InlineData("1:25 PM", true)]
        [InlineData("01:01 AM", true)]
        [InlineData("12:00 AM", true)]
        [InlineData("12:00 PM", true)]
        [InlineData("15:01", false)]
        [InlineData("15:01 PM", false)]
        [InlineData("invalid", false)]
        [InlineData("", false)]


        public void CanParseTime_ReturnsBool(string input, bool canParse) 
        {
            //Act
            var result = UserInput.CanParseTime(input);

            //Assert
            Assert.Equal(canParse, result);           
        }


        [Theory]
        [InlineData("1:25 PM", 13, 25)]
        [InlineData("01:01 AM", 1, 1)]
        [InlineData("12:00 AM", 0, 0)]
        [InlineData("12:00 PM", 12, 0)]
        [InlineData("15:01", 0, 0)]
        [InlineData("15:01 PM", 0, 0)]
        [InlineData("invalid", 0, 0)]
        [InlineData("", 0, 0)]

        public void ParseTime_ReturnsTimeOnly(string input, int expectedHour, int expectedMinute)
        {
            //Arrange 
            var expectedTime = new TimeOnly(expectedHour, expectedMinute);

            //Act
            var result = UserInput.ParseTime(input);

            //Assert
            Assert.Equal(expectedTime, result);
        }

        [Theory]
        [InlineData("01:25:01", true)]
        [InlineData("01:01:00", true)]
        [InlineData("00:00:00", true)]
        [InlineData("24:00:00", false)]
        [InlineData("12:12", false)]
        [InlineData("15:01 PM", false)]
        [InlineData("invalid", false)]
        [InlineData("", false)]

        public void CanParseTimeSpan_ReturnBool(string input, bool canParse)
        {
            //Arrange 


            //Act
            var result = UserInput.CanParseTimeSpan(input);

            //Assert
            Assert.Equal(canParse, result);
        }

        [Theory]
        [InlineData("01:25:01", 1,25,01)]
        [InlineData("01:01:00", 1,1,00)]
        [InlineData("00:00:00", 0,0,0)]
        [InlineData("24:00:00", 0,0,0)]
        [InlineData("12:12", 0,0,0)]
        [InlineData("15:01 PM", 0,0,0)]
        [InlineData("invalid", 0,0,0)]
        [InlineData("", 0,0,0)]

        public void ParseTimeSpan_ReturnTimeSpan(string input, int expectedHour, int expectedMinute, int expectedSecond)
        {
            //Arrange 
            var expectedTimespan = new TimeSpan(expectedHour, expectedMinute, expectedSecond);

            //Act
            var result = UserInput.ParseTimeSpan(input);

            //Assert
            Assert.Equal(expectedTimespan, result);
        }
    }
}
