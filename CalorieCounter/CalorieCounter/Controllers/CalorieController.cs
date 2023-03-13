using Microsoft.AspNetCore.Mvc;
using CalorieCounterAPI;
using CalorieCounterAPI.Data;
using CalorieCounterAPI.Repositories;
using CalorieCounterAPI.Models;
using CalorieCounterAPI.Interfaces;

namespace CalorieCounterAPI.Controllers
{
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
        //Gets all Items
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

    }

}