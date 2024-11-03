using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace CodingTrackerLibrary;

public static class CodingGoalAnalysisController
{
    public static List<CodingGoalAnalysis> GetCodingGoalsAnalysis(string connStr)
    {
        List<CodingGoalAnalysis> goalsAnalysis = new List<CodingGoalAnalysis>();
        try
        {
            using (IDbConnection connection = new SqliteConnection(connStr))
            {
                connection.Open();

                List<dynamic> rawAnalysis = connection.Query<dynamic>(@"SELECT g.Id, g.Name, g.StartTime, g.EndTime, g.Hours, 
                                                                ROUND(Hours - SUM((julianday(s.EndTime) - julianday(s.StartTime)) * 24)) AS HoursAway, 
                                                                ROUND((Hours - SUM((julianday(s.EndTime) - julianday(s.StartTime)) * 24))/(((julianday(g.EndTime) - julianday(g.StartTime))))) AS HoursPerDay
                                                                FROM CodingGoals g
                                                                LEFT JOIN CodingSessions s ON s.StartTime >= g.StartTime AND s.EndTime <= g.EndTime
                                                                GROUP BY g.Id, g.Name, g.StartTime, g.EndTime, g.Hours;").ToList();

                foreach (dynamic analysis in rawAnalysis)
                {
                    goalsAnalysis.Add(
                        new CodingGoalAnalysis()
                        {
                            Id = (int)analysis.Id,
                            Name = analysis.Name,
                            StartTime = Convert.ToDateTime(analysis.StartTime),
                            EndTime = Convert.ToDateTime(analysis.EndTime),
                            Hours = (int)analysis.Hours,
                            HoursAway = (int)analysis.HoursAway,
                            HoursPerDay = (int)analysis.HoursPerDay,
                        });
                }
                

                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error ocurred: {ex.Message}");
        }

        return goalsAnalysis;
    }
}
