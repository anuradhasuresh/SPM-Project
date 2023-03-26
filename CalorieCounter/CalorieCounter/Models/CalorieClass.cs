namespace CalorieCounterAPI.Models;

/// <summary>
/// Calorie Class with 4 fields - Id, Name, Age and Calorie Count
/// </summary>
public class CalorieClass
{
    public int Id { get; set; }

    public String Name { get; set; } = String.Empty;

    public int Age { get; set; }

    public int CalorieCount { get; set; }
	
	public int CurrentIntake { get; set; }
	
    public int GoalIntake { get; set; }

    public String WeightClass { get; set; } = String.Empty;
	
	
}
