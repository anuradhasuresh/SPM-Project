using System;
using CalorieCounterAPI;
using CalorieCounterAPI.Controllers;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Reflection;

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
        /// <summary>
        /// Performs analysis of average calorie intake compared to other people, average intake compared to goal intake and calculates the BMI - given a name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>an analysis object</returns>
        public Analysis GetAnalysis(string name)
        {
            Analysis analysis = new Analysis();
            ICollection<CalorieClass> items = _context.Calorie.ToList();

            // variables to get values of average, total average, current intake, gender, age, goal/ standard intake and result message
            double avgIntake = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Average(x => x.CurrentCalorieIntake);
            double avgCalories = items
                .Average(x => x.CurrentCalorieIntake);
            string gender = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Gender)
                .FirstOrDefault();
            int age = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Age)
                .FirstOrDefault();
            int height = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Height)
                .FirstOrDefault();
            int weight = items
                .Where(temp => temp.Name.ToLower() == name.ToLower())
                .Select(x => x.Weight)
                .FirstOrDefault();

            int goalIntake = _context.Goal_Intake
                .Where(a => a.Gender == gender && (a.minAge <= age && age <= a.maxAge))
                .Select(x => x.goal_Intake)
                .FirstOrDefault();

            string avgCalResult = "The average number of calories entered on this website is: " + Math.Round(avgCalories, 2) + ", while the average number of calories you have entered until now is: " + Math.Round(avgIntake, 2) + ".\n";
            string goalIntakeResult = resultMessage(goalIntake, avgIntake);
            string bmiResult = "Your Body Mass Index (BMI) is: " + Math.Round(weight / (Math.Pow(height * 0.01, 2)));

            analysis.AverageCalAnalysis = avgCalResult;
            analysis.GoalIntakeAnalysis = goalIntakeResult;
            analysis.BMIAnalysis = bmiResult;

            return analysis;
        }
        #endregion
        #region helper methods
        /// <summary>
        /// helper method to print the result message
        /// </summary>
        /// <param name="goalIntake"></param>
        /// <param name="avgIntake"></param>
        /// <returns>message with calorie intake analysis text</returns>
        public string resultMessage(int goalIntake, double avgIntake)
        {
            if (avgIntake < goalIntake)
                return "Your current calorie intake is " + (goalIntake - avgIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. If you're looking to gain weight, then you should increase your calorie intake by " + (goalIntake - avgIntake) + ".";

            else
                return "Your current calorie intake is " + (avgIntake - goalIntake) + " calories more than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. If you're looking to lose weight, then you should decrease your calorie intake by " + (avgIntake - goalIntake) + ".";
        }
        #endregion
    }
}

