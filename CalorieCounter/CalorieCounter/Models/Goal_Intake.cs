namespace CalorieCounterAPI.Models;

/// <summary>
/// Goal_Intake Class with 5 fields - Id, Name, Age, Gender and Calorie Count
/// </summary>
public class Goal_IntakeClass
{
    public int Id { get; set; }

    public String Gender { get; set; } = String.Empty;

    public int minAge { get; set; }

    public int maxAge { get; set; }

    public int goal_Intake { get; set; }

}
