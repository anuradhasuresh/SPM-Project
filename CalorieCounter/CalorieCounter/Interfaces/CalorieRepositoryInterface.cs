using System;
using CalorieCounterAPI;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

namespace CalorieCounterAPI.Interfaces
{
    /// <summary>
    /// provides an interface to the repository with methods for CRUD operations
    /// </summary>
	public interface ICalorieRepository
    {
        ICollection<CalorieClass> GetItems();
        bool CreateItem(CalorieClass item);
        bool UpdateItem(CalorieClass item);
        bool DeleteItem(int id);
        CalorieClass GetItem(int id);
        Analysis GetAnalysis(string name);
        bool Save();
	}
}

