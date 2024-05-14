using CodingTrackerV2.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace CodingTrackerV2.Data;

public class DataAccess
{
    private string connectionString;

    IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
    public DataAccess()
    {

        connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
    }

    public void CreateDatabase()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();

                tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS records (Id INTEGER PRIMARY KEY AUTOINCREMENT, DateStart TEXT, DateEnd TEXT, Duration TEXT)";

                tableCmd.ExecuteNonQuery();
            }
        }
    }

    public void InsertRecord(CodingRecord record)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO records (DateStart, DateEnd, Duration) VALUES (@DateStart, @DateEnd, @Duration)";
            connection.Execute(insertQuery, new { record.DateStart, record.DateEnd, record.Duration });
        }
    }

    public IEnumerable<CodingRecord> GetRecords()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string selectQuery = "SELECT * FROM records";

            var records = connection.Query<CodingRecord>(selectQuery);

            return records;
        }
    }

    public int UpdateRecord(CodingRecord updated)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string updateQuery = @"UPDATE records SET DateStart = @DateStart, DateEnd = @DateEnd WHERE Id = @Id";

            var response = connection.Execute(updateQuery, new { updated.DateStart, updated.DateEnd, updated.Id });
            return response;
        }
    }

    public int DeleteRecord(int recordId)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            string deleteQuery = "DELETE FROM records WHERE Id = @Id";
            int rowsAffected = connection.Execute(deleteQuery, new { Id = recordId });

            return rowsAffected;
        }
    }

    public void ResetIds()
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            List<CodingRecord> codes = connection.Query<CodingRecord>("SELECT Id FROM records ORDER BY Id").ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                connection.Execute("UPDATE records SET Id = @NewId WHERE Id = @OldId", new { NewId = i + 1, OldId = codes[i].Id });
            }
        }
    }
}
