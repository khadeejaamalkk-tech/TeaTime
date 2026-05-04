using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;

namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController:ControllerBase
    {
        
        private readonly IRoleServices _roleServices;
        public RoleController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _roleServices.GetAllRoles();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
        {
            var response = await _roleServices.CreateRole(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
