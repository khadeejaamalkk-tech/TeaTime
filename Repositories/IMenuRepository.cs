using TeaTimeDelivery.DTOs;

namespace TeaTimeDelivery.Repositories
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuDto>> GetAllMenu();
        Task<int> AddMenu(CreateMenuDto dto);
    }
}
