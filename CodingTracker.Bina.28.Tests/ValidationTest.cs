using CodingTracker.Bina28;

namespace CodingTracker.Bina._28.Tests
{
	[TestClass]
	public sealed class ValidationTest
	{
		[TestMethod]
		public void IsValidDate_ValidDate_ReturnsTrue()
		{

			// Arrange 
			//Valid format : yyyy-MM-dd
			string validDate = "2023-10-01";
			bool expected = true; 

			// Act
			bool result = Validation.IsValidDate(validDate);
			// Assert
			Assert.AreEqual(expected, result); 
		}

		[TestMethod]
		public void IsValidDate_InvlaidDate_ReturnsFalse()
		{

			// Arrange
			string validDate = "2023-13-01";
			bool expected = false;

			// Act
			bool result = Validation.IsValidDate(validDate);
			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void IsValidTime_ValidTime_ReturnsTrue()
		{
			// Arrange
			//Valid format : HH:mm
			string validTime = "14:30";
			bool expected = true;
			// Act
			bool result = Validation.IsValidTime(validTime);
			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void IsValidTime_InvalidTime_ReturnsFalse()
		{
			// Arrange
			string invalidTime = "25:00";
			bool expected = false;
			// Act
			bool result = Validation.IsValidTime(invalidTime);
			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void IsValidTimeRange_ValidRange_ReturnsTrue()
		{
			// Arrange
			string startTime = "10:00";
			string endTime = "12:00";
			bool expected = true;
			// Act
			bool result = Validation.IsValidTimeRange(startTime, endTime);
			// Assert
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void IsValidTimeRange_InvalidRange_ReturnsFalse()
		{
			// Arrange
			string startTime = "14:00";
			string endTime = "12:00";
			bool expected = false;
			// Act
			bool result = Validation.IsValidTimeRange(startTime, endTime);
			// Assert
			Assert.AreEqual(expected, result);
		}
	}
}
