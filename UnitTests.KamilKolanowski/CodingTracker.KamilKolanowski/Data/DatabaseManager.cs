using CodingTracker.KamilKolanowski.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace CodingTracker.KamilKolanowski.Data;

internal class DatabaseManager
{
    private string _connectionString;
    internal DatabaseManager()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        
        _connectionString = config.GetConnectionString("DatabaseConnection");
    }
    internal class CodingSession
    {
        internal int Id { get; set; }
        internal DateTime StartDateTime { get; set; }
        internal DateTime EndDateTime { get; set; }
        internal decimal Duration { get; set; }
    }

    internal class CodingReport
    {
        internal string Period { get; set; }
        internal decimal TimeSpent { get; set; }
    }
    
    private SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
    
    internal List<CodingSession> ReadTable()
    {
        var connection = CreateConnection();
        connection.Open();
        
        string query = $"SELECT * FROM CodingTracker.TCSA.CodingSessions";
        var sessions = connection.Query<CodingSession>(query).ToList();

        return sessions;
    }

    internal void WriteTable(DateTime startDateTime, DateTime endDateTime, decimal duration)
    {
        var connection = CreateConnection();
        connection.Open();
        
        string query = @$"INSERT INTO CodingTracker.TCSA.CodingSessions (StartDateTime, EndDateTime, Duration) 
                       VALUES (@StartDateTime, @EndDateTime, @Duration)";
        
        connection.Execute(query, new { StartDateTime = startDateTime, EndDateTime = endDateTime, Duration = duration });
    }

    internal List<CodingReport> CreateReport(Options.ReportingOptions reportingOptions, string orderingReport)
    {
        var connection = CreateConnection();
        connection.Open();

        string selectList = "";
        string groupingCol = "";
        string orderingCol = "";
        
        switch (reportingOptions)
        {
            case Options.ReportingOptions.GetWeeklyReport:
                groupingCol = "DATEPART(year, StartDateTime), DATEPART(week, StartDateTime)";
                selectList = $"CONCAT('Week ', DATEPART(week, StartDateTime), ' of ', DATEPART(year, StartDateTime))";
                orderingCol =
                    $"DATEPART(year, StartDateTime) {orderingReport}, DATEPART(week, StartDateTime) {orderingReport}";
                break;
            case Options.ReportingOptions.GetMonthlyReport:
                groupingCol = "DATEPART(year, StartDateTime), DATEPART(month, StartDateTime)";
                selectList = $"CONCAT('Month ', DATEPART(month, StartDateTime), ' of ', DATEPART(year, StartDateTime))";
                orderingCol =
                    $"DATEPART(year, StartDateTime) {orderingReport}, DATEPART(month, StartDateTime) {orderingReport}";
                break;
            case Options.ReportingOptions.GetYearlyReport:
                groupingCol = "DATEPART(year, StartDateTime)";
                selectList = "DATEPART(year, StartDateTime)";
                orderingCol = $"DATEPART(year, StartDateTime) {orderingReport}";
                break;
        }
        
        string query = $"SELECT {selectList} AS Period, SUM(Duration) AS TimeSpent FROM CodingTracker.TCSA.CodingSessions GROUP BY {groupingCol} ORDER BY {orderingCol}";
        
        var report = connection.Query<CodingReport>(query).ToList();
        return report;
    }
    
}