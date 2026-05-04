using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Controllers
{
   
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IUserRepository _userRepository;
            private readonly JwtService _jwtService;

            public AuthController(IUserRepository userRepository, JwtService jwtService)
            {
                _userRepository = userRepository;
                _jwtService = jwtService;
            }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepository.GetUserByUsername(dto.Username);

            if (user == null || user.Password != dto.Password)
                return Unauthorized("Invalid username or password");


            var result = _jwtService.GenerateToken(user.Id, user.RoleId);

            return Ok(result);
        }
    }
    }

