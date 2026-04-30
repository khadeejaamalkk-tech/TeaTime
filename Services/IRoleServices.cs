using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface IRoleServices
    {
        Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRoles();
        Task<ApiResponse<RoleDto>> CreateRole(CreateRoleDto dto);
    }
}
