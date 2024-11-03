using CodingTrackerLibrary;

namespace CodingTracker;

public class CodingGoalOrganizer
{
    public static void ShowCodingGoals(string connectionString)
    {
        List<CodingGoal> goals = CodingGoalController.GetCodingGoals(connectionString);

        OutputRenderer.ShowTable<CodingGoal>(goals, title: "Coding Goals");
    }

    public static void ShowCodingGoalsAnalysis(string connectionString)
    {
        List<CodingGoalAnalysis> codingGoalAnalysis = CodingGoalAnalysisController.GetCodingGoalsAnalysis(connectionString);

        OutputRenderer.ShowTable<CodingGoalAnalysis>(codingGoalAnalysis, title: "Coding Goals Analysis");
    }
}
