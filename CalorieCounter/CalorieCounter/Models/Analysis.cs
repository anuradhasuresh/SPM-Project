namespace CalorieCounterAPI.Models;

/// <summary>
/// Analysis Class with 3 fields - AverageCalAnalysis, GoalIntakeAnalysis and BMIAnalysis
/// </summary>
public class Analysis
{
    public String AverageCalAnalysis { get; set; } = String.Empty;

    public String GoalIntakeAnalysis { get; set; } = String.Empty;

    public String BMIAnalysis { get; set; } = String.Empty;
}
