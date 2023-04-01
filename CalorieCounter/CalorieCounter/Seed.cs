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
                new CalorieClass {Id = 1, Name = "James Smith", Age = 26, CurrentCalorieIntake = 3500, Gender = "M"},
                new CalorieClass {Id = 2, Name = "Emma Anderson", Age = 28, CurrentCalorieIntake = 2900, Gender = "F"},
        };
                dataContext.Calorie.AddRange(items);
                dataContext.SaveChanges();
            }
        }
    }
}
