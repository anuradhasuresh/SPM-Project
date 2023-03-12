using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

namespace CalorieCounterAPI
{
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
                new CalorieClass {Id = 1, Name = "James Smith", Age = 26, CalorieCount = 3500},
                new CalorieClass {Id = 1, Name = "Emma Anderson", Age = 28, CalorieCount = 2900},
        };
                dataContext.Calorie.AddRange(items);
                dataContext.SaveChanges();
            }
        }
    }
}
