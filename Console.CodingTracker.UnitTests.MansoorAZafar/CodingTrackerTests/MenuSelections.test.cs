using CodingTrackerLibrary.Models;

namespace CodingTrackerTests;

[TestClass]
public class MenuSelectionsTest
{
    [TestMethod]
    public void Test_MenuSelectionItems_Names()
    {
        string[] selections = Enum.GetNames<MenuSelections>();
        string[] expectedNames = 
            { 
                "exit", 
                "update", 
                "delete", 
                "insert", 
                "data", 
                "reports", 
                "goals",
                "None",
            };
    
        for(int i = 0; i < expectedNames.Length; ++i)
            Assert.AreEqual(expectedNames[i], selections[i]);
    }

    [TestMethod]
    public void Test_MenuSelectionItems_Values()
    {
        int[] selectionValues = (int[])Enum.GetValuesAsUnderlyingType<MenuSelections>();
        int[] expectedValues = { 0, 1, 2, 3, 4, 5, 6, -1 };

        for (int i = 0; i < expectedValues.Length; ++i)
            Assert.AreEqual(expectedValues[i], selectionValues[i]);

    }


    [TestMethod]
    public void Test_ReportSelections_Names()
    {
        string[] selections = Enum.GetNames<ReportSelections>();
        string[] expectedNames =
            {
                "exit", 
                "startFromXDaysAgo", 
                "dateToToday",
                "totalAllTime", 
                "startToEnd", 
                "totalForMonth", 
                "filter",
                "None",
            };

        for (int i = 0; i < expectedNames.Length; ++i)
            Assert.AreEqual(expectedNames[i], selections[i]);
    }

    [TestMethod]
    public void Test_ReportSelections_Values()
    {
        int[] selectionValues = (int[])Enum.GetValuesAsUnderlyingType<ReportSelections>();
        int[] expectedValues = { 0, 1, 2, 3, 4, 5, 6, -1 };

        for (int i = 0; i < expectedValues.Length; ++i)
            Assert.AreEqual(expectedValues[i], selectionValues[i]);

    }


    [TestMethod]
    public void Test_SortingSelections_Names()
    {
        string[] selections = Enum.GetNames<SortingSelections>();
        string[] expectedNames =
            {
                "ASC",
                "DESC",
            };

        for (int i = 0; i < expectedNames.Length; ++i)
            Assert.AreEqual(expectedNames[i], selections[i]);
    }

    [TestMethod]
    public void Test_SortingSelections_Values()
    {
        int[] selectionValues = (int[])Enum.GetValuesAsUnderlyingType<SortingSelections>();
        int[] expectedValues = { 0,1 };

        for (int i = 0; i < expectedValues.Length; ++i)
            Assert.AreEqual(expectedValues[i], selectionValues[i]);

    }


    [TestMethod]
    public void Test_PeriodSelection_Names()
    {
        string[] selections = Enum.GetNames<Period>();
        string[] expectedNames =
            {
                "Hours",
                "Days",
                "Weeks"
            };

        for (int i = 0; i < expectedNames.Length; ++i)
            Assert.AreEqual(expectedNames[i], selections[i]);
    }

    [TestMethod]
    public void Test_PeriodSelections_Values()
    {
        int[] selectionValues = (int[])Enum.GetValuesAsUnderlyingType<Period>();
        int[] expectedValues = { 1, 1*24, 1*24*7 };

        for (int i = 0; i < expectedValues.Length; ++i)
            Assert.AreEqual(expectedValues[i], selectionValues[i]);

    }
}
