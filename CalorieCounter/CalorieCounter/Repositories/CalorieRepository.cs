using System;
using CalorieCounterAPI;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CalorieCounterAPI.Repositories
{
	public class CalorieRepository : ICalorieRepository
	{
        private DataContext _context;

        /// <summary>
        /// constructor to initialize DataContext
        /// </summary>
        /// <param name="context"></param>
        public CalorieRepository(DataContext context)
        {
            _context = context;
        }

        #region methods to perform CRUD operations
        /// <summary>
        /// Function to get all the entries or items present in the MySQL database
        /// </summary>
        /// <returns>A list of all items present</returns>
        public ICollection<CalorieClass> GetItems()
        {
            return _context.Calorie.ToList();
        }
        /// <summary>
        /// Function to create or add a new item or entry into the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the item has been added and false if not</returns>
        public bool CreateItem(CalorieClass item)
        {
            _context.Add(item);
            return Save();
        }
        /// <summary>
        /// updates or edits an item in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the item has been updated and false if not</returns>
        public bool UpdateItem(CalorieClass item)
        {
            _context.Update(item);
            return Save();
        }
        /// <summary>
        /// deletes an item in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns>true if the item has been deleted and false if not</returns>
        public bool DeleteItem(int itemId)
        {
            _context.Remove(GetItems().FirstOrDefault(a => a.Id == itemId));
            return Save();
        }
        /// <summary>
        /// Function to save changes made to the database
        /// </summary>
        /// <returns>true if changes have been saved and false if not</returns>
        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved == 1;
        }
        #endregion
    }
}

