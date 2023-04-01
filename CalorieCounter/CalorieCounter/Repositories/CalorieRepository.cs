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
        /// Function to get a particular item from the MySQL database based on an ID entered by the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Details of that item with that particular ID</returns>
        public CalorieClass GetItem(int id)
        {
            return _context.Calorie.Where(item => item.Id == id).FirstOrDefault();
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
        #region analysis method
        public string GetAnalysis(string name)
        {
            ICollection<CalorieClass> items = _context.Calorie.ToList();
            double avg = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Average(x => x.CurrentCalorieIntake);
            double avgCalories = items
                .Average(x => x.CurrentCalorieIntake);
            int currentIntake = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.CurrentCalorieIntake)
                .FirstOrDefault();
            string gender = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Gender)
                .FirstOrDefault();
            int age = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Age)
                .FirstOrDefault();

            int goalIntake;
            string result = "The average number of calories entered on this website is: " + Math.Round(avgCalories, 2) + ", while the average number of calories you have entered until now is: " + Math.Round(avg, 2) + "\n \n";

            if (Enumerable.Range(9, 13).Contains(age))
            {
                switch (gender)
                {
                    case "M":
                        {
                            goalIntake = 1800;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                    case "F":
                        {
                            goalIntake = 1500;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                }
            }
            else if (Enumerable.Range(14, 18).Contains(age))
            {
                switch (gender)
                {
                    case "M":
                        {
                            goalIntake = 2200;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                    case "F":
                        {
                            goalIntake = 1800;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                }
            }
            else if (Enumerable.Range(19, 30).Contains(age))
            {
                switch (gender)
                {
                    case "M":
                        {
                            goalIntake = 2500;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                    case "F":
                        {
                            goalIntake = 2000;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                }
            }
            else if (Enumerable.Range(31, 50).Contains(age))
            {
                switch (gender)
                {
                    case "M":
                        {
                            goalIntake = 2300;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                    case "F":
                        {
                            goalIntake = 1800;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                }
            }
            else
            {
                switch (gender)
                {
                    case "M":
                        {
                            goalIntake = 2100;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                    case "F":
                        {
                            goalIntake = 1600;
                            if (currentIntake < goalIntake)
                                result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";

                            else
                                result += "Your current calorie intake is " + (currentIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
                            break;
                        }
                }
            }
            return result;
        }
        #endregion
    }
}

