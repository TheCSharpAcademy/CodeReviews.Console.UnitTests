using PhoneBook;
namespace PhoneBookTests;

[TestClass]
public class PhoneBookTest
{
    [TestMethod]
    [DataRow("+38978333333")]
    public void ValidateNumber_IsValidNumber_ReturnTrue(string phoneNumber)
    {
        var result = PhoneBookValidation.ValidateNumber(phoneNumber);
        Assert.IsTrue(result);
    }
    [TestMethod]
    [DataRow("389783333333")]
    [DataRow("")]
    public void ValidateNumber_IsValidNumber_ReturnFalse(string phoneNumber)
    {
        var result = PhoneBookValidation.ValidateNumber(phoneNumber);
        Assert.IsFalse(result);
    }
    [TestMethod]
    [DataRow("email@gmail.com")]
    public void ValidateEmail_IsValidEmail_ReturnTrue(string email)
    {
        var result = PhoneBookValidation.ValidateEmail(email);
        Assert.IsTrue(result);
    }
    [TestMethod]
    [DataRow("email..com")]
    public void ValidateEmail_IsValidEmail_ReturnFalse(string email)
    {
        var result = PhoneBookValidation.ValidateEmail(email);
        Assert.IsFalse(result);
    }
    [TestMethod]
    [DataRow("3434")]
    public void ValidateCategory_IsInvalidCategory_ReturnTrue(string category)
    {
        var result = PhoneBookValidation.ValidateCategory(category);
        Assert.IsTrue(result);
    }
    [TestMethod]
    [DataRow("Work")]
    public void ValidateCategory_IsInvalidCategory_ReturnFalse(string category)
    {
        var result = PhoneBookValidation.ValidateCategory(category);
        Assert.IsFalse(result);
    }
}