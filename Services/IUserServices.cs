using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface IUserServices
    {
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers();
        Task<ApiResponse<UserDto>> GetUserById(int id);
        Task<ApiResponse<UserDto>> GetUserByUsername(string username);
        Task<ApiResponse<UserDto>> CreateUser(CreateUserDto dto);
        Task<ApiResponse<UserDto>> UpdateUser(int id, UpdateUserDto dto);
        Task<ApiResponse<UserDto>> DeleteUser(int id);
    }
}
