using Microsoft.VisualStudio.TestTools.UnitTesting;
using cacheMe512.CodeTracker;

namespace CodeTracking.Tests;

[TestClass]
public class ValidationTests
{
    [TestMethod]
    public void DateTimeInSequence_ValidDates_ReturnsTrue()
    {
        DateTime startTime = new DateTime(2025, 3, 1, 12, 0, 0);
        DateTime endTime = new DateTime(2025, 3, 1, 14, 0, 0);

        bool result = Validation.DateTimeInSequence(startTime, endTime);

        Assert.IsTrue(result);
    }
}
