using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

namespace CalorieCounterAPI.Controllers
{
    /// <summary>
    /// controller class for CRUD operations
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CalorieController : ControllerBase
    {
        static double AvgCalorie = 3.5;
        private readonly ILogger<CalorieController> _logger;
        private readonly ICalorieRepository _calorieRepository;

        public CalorieController(ILogger<CalorieController> logger, ICalorieRepository calorieRepository)
        {
            _logger = logger;
            _calorieRepository = calorieRepository;

        }
        /// <summary>
        /// API call to get/ display all records in DB
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllItems")]
        [ProducesResponseType(200, Type = typeof(List<CalorieClass>))]
        public IActionResult GetItems()
        {
            _logger.Log(LogLevel.Information, "Get items");
            return Ok(_calorieRepository.GetItems());
        }
   
        //Adds an item
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult CreateItem([FromBody] CalorieClass item)
        {
            _logger.Log(LogLevel.Information, "Add an item");
            if (item == null)
                return BadRequest("Item is null");

            bool result = _calorieRepository.CreateItem(item);

            if (result)
                return Ok("Successfully added");
            else
                return BadRequest("Item not added");
        }
        //Gets a particular Item based on Id
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(CalorieClass))]
        [ProducesResponseType(400)]
        public IActionResult GetItem(int id)
        {
            _logger.Log(LogLevel.Information, "Get a particular item");
            CalorieClass item = _calorieRepository.GetItem(id);

            if (item == null)
                return NotFound();
            else
                return Ok(item);
        }
        //Updates or edits an item 
        [HttpPut()]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]

        public IActionResult UpdateItem([FromBody] CalorieClass item)
        {
            _logger.Log(LogLevel.Information, "Update an item");
            if (item == null)
                return BadRequest("Item is null");

            bool isUpdated = _calorieRepository.UpdateItem(item);

            if (!isUpdated)
                return NotFound("No matching item");
            else
                return Ok("Successfully updated");
        }

        // Delete item
        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]

        public IActionResult DeleteItem(int itemId)
        {
            _logger.Log(LogLevel.Information, "Deleted Item");
            if (itemId == 0)
                return BadRequest("ItemId is null");

            bool result = _calorieRepository.DeleteItem(itemId);

            if (result)
                return Ok("Successfully deleted");
            else
                return BadRequest("Item not deleted");
        }

		[HttpGet("/Analysis/{name}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetAvgcaloriebyname(string name)
		{
            // return Ok(_calorieRepository.GetAnalysis());
            ICollection<CalorieClass> items = _calorieRepository.GetItems();
			double avg = items
				//.Skip(Math.Max(0, items.Count() - 5))
				.Where(temp => temp.Name.ToLower() == name.ToLower())
				.Average(x => x.CurrentCalorieIntake);
            double avgCalorie = items
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
            string result = "Average Calorie: " + avgCalorie + "\nYour Average: " + avg + "\n";

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

                            result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
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

                            result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
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

                            result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
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

                            result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
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

                            result += "Your current calorie intake is " + (goalIntake - currentIntake) + " calories less than your goal intake of " + goalIntake + " for a sedentary lifestyle for your age group and gender. ";
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
            return Ok(result);
		}

	}

}