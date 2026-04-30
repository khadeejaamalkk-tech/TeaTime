using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<ApiResponse<IEnumerable<MenuDto>>> GetAllMenu()
        {
            try
            {
                var menuList = await _menuRepository.GetAllMenu();

                if (menuList == null || !menuList.Any())
                {
                    return ApiResponse<IEnumerable<MenuDto>>.NotFound("No menu items found");
                }

                return ApiResponse<IEnumerable<MenuDto>>.Success(menuList);
            }
            catch (Exception)
            {
                return ApiResponse<IEnumerable<MenuDto>>.BadRequest("Error fetching menu");
            }
        }
        public async Task<ApiResponse<int>> AddMenu(CreateMenuDto dto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Name))
                    return ApiResponse<int>.BadRequest("Name is required");

                if (dto.Price <= 0)
                    return ApiResponse<int>.BadRequest("Price must be greater than 0");

                var id = await _menuRepository.AddMenu(dto);

                return ApiResponse<int>.Success(id);
            }
            catch (Exception)
            {
                return ApiResponse<int>.BadRequest("Error adding menu");
            }
        }

    }
}
