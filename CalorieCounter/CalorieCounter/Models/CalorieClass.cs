//namespace CalorieCounterAPI.Models;

//public class CalorieClass
//{
//    // Class for file data - each row or item of data in the file has an ID, Name, Age and CalorieCount
//    private static int nextId = 1;

//    public CalorieClass(String name, Int32 age, Int32 calorieCount)
//    {
//        Name = name;
//        Age = age;
//        CalorieCount = calorieCount;
//        Id = nextId++;
//    }
//    public int Id { get; set; }

//    public String Name { get; set; } = String.Empty;

//    public int Age { get; set; }

//    public int CalorieCount { get; set; }

//}

namespace CalorieCounterAPI.Models;

public class CalorieClass
{
    public int Id { get; set; }

    public String Name { get; set; } = String.Empty;

    public int Age { get; set; }

    public int CalorieCount { get; set; }
}
