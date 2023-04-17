namespace CalorieCounterAPI.Models;

/// <summary>
/// Calorie Class with 5 fields - Id, Name, Age, Gender and Calorie Count
/// </summary>
public class CalorieClass
{
    public int Id { get; set; }

    public String Name { get; set; } = String.Empty;

    public int Age { get; set; }

    public int CurrentCalorieIntake { get; set; }

    public String Gender { get; set; } = String.Empty;

    public int Height { get; set; }

    public int Weight { get; set; }
}
