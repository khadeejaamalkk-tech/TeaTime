using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;

namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var response = await _restaurantService.GetAllRestaurants();
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantById(int id)
        {
            var response = await _restaurantService.GetRestaurantById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var response = await _restaurantService.CreateRestaurant(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, [FromBody] UpdateRestaurantDto dto)
        {
            var response = await _restaurantService.UpdateRestaurant(id, dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var response = await _restaurantService.DeleteRestaurant(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}