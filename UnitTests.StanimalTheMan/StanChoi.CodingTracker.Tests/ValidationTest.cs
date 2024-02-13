namespace StanChoi.CodingTracker.Tests;

[TestClass]
public class ValidationTest
{
	[TestMethod]
	[DataRow("fffs")]
	[DataRow(null)]
	[DataRow("")]
	[DataRow("-1")]
	public void WhenIdIsInvalidAssertFalse(string id)
	{
		var result = Validation.IsValidId(id);

		Assert.IsFalse(result);
	}

	[TestMethod]
	[DataRow("0")]
	[DataRow("14")]
	public void WhenIdIsValidAssertTrue(string id)
	{
		var result = Validation.IsValidId(id);

		Assert.IsTrue(result);
	}

	[TestMethod]
	[DataRow("02/23/23")]
	[DataRow("ffsf-12-22 22:45")]
	[DataRow("2022-54-33 32:45")]
	public void WhenDateTimeIsNotValidAssertFalse(string dateTimeString)
	{
		var result = Validation.IsValidDateTime(dateTimeString);
		Assert.IsFalse(result);
	}

	[TestMethod]
	[DataRow("2024-02-12 19:10")]
	public void WhenDateTimeIsValidAssertTrue(string dateTimeString)
	{
		var result = Validation.IsValidDateTime(dateTimeString);
		Assert.IsTrue(result);
	}
}