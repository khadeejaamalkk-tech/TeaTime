using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.DTOs;

namespace TeaTimeDelivery.Services
{
    public interface IMenuService
    {
        Task<ApiResponse<IEnumerable<MenuDto>>> GetAllMenu();
        Task<ApiResponse<int>> AddMenu(CreateMenuDto dto);
    }
}
