using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;

namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController:ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController (IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMenu()
        {
            var response = await _menuService.GetAllMenu();
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenu([FromBody] CreateMenuDto dto)
        {
            var response = await _menuService.AddMenu(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}
