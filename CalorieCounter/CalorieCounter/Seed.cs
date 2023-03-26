using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

namespace CalorieCounterAPI
{
    /// <summary>
    /// class to provide initial DB context
    /// </summary>
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public void SeedDataContext()
        {
            if (!dataContext.Calorie.Any())
            {
			   List<CalorieClass> items = new() {
			   new CalorieClass {Id = 1, Name = "James Smith", Age = 26, CalorieCount = 3500, CurrentIntake = 3500, GoalIntake = 2500, WeightClass = "loss" },
			   new CalorieClass {Id = 2, Name = "Emma Anderson", Age = 28, CalorieCount = 2900, CurrentIntake = 2900, GoalIntake = 3600, WeightClass = "gain"},
			   new CalorieClass {Id = 3, Name = "Jackson Wang", Age = 43, CalorieCount = 2100, CurrentIntake = 2100, GoalIntake = 4000, WeightClass = "gain"},
			         
        };
                dataContext.Calorie.AddRange(items);
                dataContext.SaveChanges();
            }
        }
    }
}
