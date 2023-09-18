using CodingTracker.Furiax;
using Microsoft.Data.Sqlite;

namespace CodingTrackerTesting
{
	[TestClass]
	public class ValidationTests
	{
		[TestMethod]
		[DataRow("18/09/23 13:01", true)]
		[DataRow("31/12/26 12:00", false)]
		[DataRow("01/01/22 00:00", true)]
		[DataRow("18/09/23", false)]
		[DataRow("31/12/26 32:00", false)]
		[DataRow("31/12/26 12:61", false)]
		[DataRow("", false)]
		[DataRow(" ", false)]
		[DataRow("1", false)]
		[DataRow("string", false)]
		public void Is_Inputted_Date_A_Valid_Date(string input, bool expectedResult)
		{
			bool result = Validation.ValidateDate(input);
			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		[DataRow("1", true)]
		[DataRow("5", true)]
		[DataRow("0", true)]
		[DataRow("-2", false)]
		[DataRow("", false)]
		[DataRow(" ", false)]
		[DataRow("3,14", false)]
		[DataRow("string", false)]
		[DataRow("c", false)]
		public void Is_Inputted_Id_A_Valid_Id(string input, bool expectedResult)
		{
			bool result = Validation.ValidateId(input);

			Assert.AreEqual(expectedResult, result);
		}

		[TestMethod]
		[DataRow("2", true)]
		[DataRow("265", true)]
		[DataRow("0", false)]
		[DataRow("3,14", false)]
		[DataRow("abcde", false)]
		[DataRow("f", false)]
		[DataRow("", false)]
		[DataRow(" ", false)]
		public void Is_Input_A_Valid_Integer(string input, bool expectedResult)
		{
			bool result = Validation.ValidInteger(input);

			Assert.AreEqual(expectedResult, result);
		}

		public static bool CheckIfRecordExists(int recordId, string connectionString)
		{
			using (var connection = new SqliteConnection(connectionString))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = $"SELECT * FROM CodeTracker WHERE Id = '{recordId}'";
				SqliteDataReader reader = command.ExecuteReader();
				if (reader.HasRows)
					return true;
				else
					return false;
			}
		}

		[TestMethod]
		[DataRow(1, true)]
		[DataRow(2, false)]
		[DataRow(10, false)]
		[DataRow(15, true)]
		[DataRow(25, true)]
		[DataRow(28, false)]
		[DataRow(41, false)]
		public void Does_RecordExistst_Return_The_Right_Result(int id, bool expectedResult)
		{
			string dataDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string relativePath = @"..\..\..\..\CodingTracker.Furiax\CodeTracker.db";
			string dbPath = Path.GetFullPath(Path.Combine(dataDirectory, relativePath));
			string connection = $"Data Source={dbPath}";


			bool result = Validation.CheckIfRecordExists(id, connection);
			Assert.AreEqual(result, expectedResult);
		}

	}

}