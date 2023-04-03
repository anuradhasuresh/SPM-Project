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
        /// <returns>list of items</returns>
        [HttpGet("GetAllItems")]
        [ProducesResponseType(200, Type = typeof(List<CalorieClass>))]
        public IActionResult GetItems()
        {
            _logger.Log(LogLevel.Information, "Get items");
            return Ok(_calorieRepository.GetItems());
        }
   
        /// <summary>
        /// Adds an item to the DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns>response message with success or error</returns>
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
        /// <summary>
        /// Gets a particular Item based on Id from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns>item</returns>
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
        /// <summary>
        /// Updates or edits an existing item in the DB
        /// </summary>
        /// <param name="item"></param>
        /// <returns>success or error message</returns>        
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
        /// <summary>
        /// Deletes an existing item in the DB
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>success or error message</returns>
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
        /// <summary>
        /// Perform analysis based on the name entered
        /// </summary>
        /// <param name="name"></param>
        /// <returns>analysis</returns>
		[HttpGet("/Analysis/{name}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public IActionResult GetAnalysis(string name)
		{
             return Ok(_calorieRepository.GetAnalysis(name));
		}
	}
}