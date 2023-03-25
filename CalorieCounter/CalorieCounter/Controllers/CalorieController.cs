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


			//return Ok(_calorieRepository.GetItems().Where(temp => temp.Name.ToLower() == name.ToLower()).Average(x => x.CalorieCount));
			ICollection<CalorieClass> items = _calorieRepository.GetItems();
			double avg = items
				//.Skip(Math.Max(0, items.Count() - 5))
				.Where(temp => temp.Name.ToLower() == name.ToLower())
				.Average(x => x.CalorieCount);

			double diff = AvgCalorie - avg;
			string result = "Average Calorie: " + AvgCalorie + "\nYour Average: " + avg + "\n";

			if (diff > 0)
			{
				result += "You took " + Math.Round(Math.Abs(diff), 2) + " calories less than the average human.";
			}
			else
			{
				result += "You took " + Math.Round(Math.Abs(diff), 2) + " calories more than the average human.";
			}


			return Ok(result);
		}

	}

}