using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;


namespace TeaTimeDelivery.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
        public class UserController : ControllerBase
        {
            private readonly IUserServices _userService;

            public UserController(IUserServices userService)
            {
                _userService = userService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAllUsers()
            {
                var response = await _userService.GetAllUsers();
                return StatusCode(response.StatusCode, response);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetUserById(int id)
            {
                var response = await _userService.GetUserById(id);
                return StatusCode(response.StatusCode, response);
            }

            [HttpPost]
            public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
            {
                var response = await _userService.CreateUser(dto);
                return StatusCode(response.StatusCode, response);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
            {
                var response = await _userService.UpdateUser(id, dto);
                return StatusCode(response.StatusCode, response);
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteUser(int id)
            {
                var response = await _userService.DeleteUser(id);
                return StatusCode(response.StatusCode, response);
            }
        }
    }

