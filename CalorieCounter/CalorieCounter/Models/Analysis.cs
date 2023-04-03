namespace CalorieCounterAPI.Models;

/// <summary>
/// Analysis Class with 3 fields - AverageAnalysis, GoalAnalysis and WeightAnalysis
/// </summary>
public class Analysis
{
    public String AverageAnalysis { get; set; } = String.Empty;

    public String GoalAnalysis { get; set; } = String.Empty;

    public String WeightAnalysis { get; set; } = String.Empty;
}
