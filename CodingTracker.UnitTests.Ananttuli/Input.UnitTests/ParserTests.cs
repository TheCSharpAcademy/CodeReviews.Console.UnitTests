namespace Input.UnitTests;

[TestClass]
public class ParserTests
{
    public static IEnumerable<object[]> ValidDecimals()
    {
        return new List<object[]>
        {
            new object[] { "123",           123m },
            new object[] { "0.001",         0.001m },
            new object[] { "-0.00000001",   -0.00000001m },
            new object[] { "100000000000",  100000000000m }
        };
    }

    [DataTestMethod]
    [DynamicData(nameof(ValidDecimals), DynamicDataSourceType.Method)]
    public void ParseDecimal_ValidNumericalString_ReturnSuccessDecimal(string validInput, decimal expectedValue)
    {
        var (success, parsedValue) = Parser.ParseDecimal(validInput);

        Assert.AreEqual(success, true);
        Assert.AreEqual(parsedValue, expectedValue);
    }

    [TestMethod]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("123garbage")]
    [DataRow("garbage123.23")]
    [DataRow("/''''/////''''")]
    [DataRow("\r\n")]
    public void ParseDecimal_InvalidNumericalString_ReturnFailed(string invalidInput)
    {
        var (success, _) = Parser.ParseDecimal(invalidInput);

        Assert.AreEqual(success, false);
    }


    [TestMethod]
    public void ParseDateTimeExactFormat_ValidDateTimeString_ReturnSuccessDateTime()
    {
        var expectedDateTime = new DateTime(2022, 1, 24, 1, 12, 00);

        var (success, parsedValue) = Parser.ParseDateTimeExactFormat("2022-01-24 01:12", "yyyy-MM-dd HH:mm");

        Assert.AreEqual(success, true);
        Assert.AreEqual(parsedValue, expectedDateTime);
    }

    [TestMethod]
    [DataRow("2022-JAN-12")]
    [DataRow("2022-12-12")]
    [DataRow("2022-12-12 1:42pm")]
    [DataRow("2022-12-12 1:01")]
    [DataRow("2022-12-12 25:12")]
    [DataRow("2022-24-12 00:00")]
    [DataRow("")]
    [DataRow(" ")]
    [DataRow("garbage")]
    [DataRow("\r\n")]
    [DataRow("\n")]
    public void ParseDateTimeExactFormat_InvalidDateTimeString_ReturnFail(string invalidInput)
    {
        var (success, _) = Parser.ParseDateTimeExactFormat(invalidInput, "yyyy-MM-dd HH:mm");

        Assert.AreEqual(success, false);
    }
}