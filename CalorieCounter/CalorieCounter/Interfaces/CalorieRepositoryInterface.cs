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
        //CalorieClass GetItem(int id);
        //bool ItemExists(int id);
        bool CreateItem(CalorieClass item);
        bool UpdateItem(CalorieClass item);
        bool DeleteItem(int id);
        //Dictionary<string, dynamic> GetAnalysis();
        bool Save();
        
	}
}

