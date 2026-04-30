using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;
using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
    
    public async Task<ApiResponse<IEnumerable<RoleDto>>> GetAllRoles()
        {
            try
            {
                var roles = await _roleRepository.GetAllRoles();
                var result = roles.Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    IsActive = r.IsActive
                });
                return ApiResponse<IEnumerable<RoleDto>>.Success(result);

            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<RoleDto>>.Error(ex.Message);
            }
        }


        public async Task<ApiResponse<RoleDto>> CreateRole(CreateRoleDto dto)
        {
            try
            {
                var role = new Role
                {
                    RoleName = dto.RoleName,
                    IsActive = dto.IsActive
                };
                await _roleRepository.CreateRole(role);
                return ApiResponse<RoleDto>.Created(null, "Role created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<RoleDto>.Error(ex.Message);
            }
        }
    }
}
