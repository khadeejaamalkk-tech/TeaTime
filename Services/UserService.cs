using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class UserService :IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _userRepository.GetAllUsers();

                var result = users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    RoleId = u.RoleId,
                    RoleName = u.RoleName,
                    RestaurantId = u.RestaurantId,
                    RestaurantName = u.RestaurantName,
                    IsActive = u.IsActive
                });

                return ApiResponse<IEnumerable<UserDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<UserDto>>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                    return ApiResponse<UserDto>.NotFound("User not found");

                var result = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    RoleId = user.RoleId,
                    RoleName = user.RoleName,
                    RestaurantId = user.RestaurantId,
                    RestaurantName = user.RestaurantName,
                    IsActive = user.IsActive
                };

                return ApiResponse<UserDto>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<UserDto>> GetUserByUsername(string username)
        {
            try
            {
                var user = await _userRepository.GetUserByUsername(username);

                if (user == null)
                    return ApiResponse<UserDto>.NotFound("User not found");

                var result = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    RoleId = user.RoleId,
                    RoleName = user.RoleName,
                    RestaurantId = user.RestaurantId,
                    RestaurantName = user.RestaurantName,
                    IsActive = user.IsActive
                };

                return ApiResponse<UserDto>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<UserDto>> CreateUser(CreateUserDto dto)
        {
            try
            {
                
                var existing = await _userRepository.GetUserByUsername(dto.Username);
                if (existing != null)
                    return ApiResponse<UserDto>.BadRequest("Username already exists");

                var user = new User
                {
                    Username = dto.Username,
                    Password = dto.Password,  
                    RoleId = dto.RoleId,
                    RestaurantId = dto.RestaurantId,
                    IsActive = true
                };

                await _userRepository.CreateUser(user);

                return ApiResponse<UserDto>.Created(null, "User created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<UserDto>> UpdateUser(int id, UpdateUserDto dto)
        {
            try
            {
                var existing = await _userRepository.GetUserById(id);
                if (existing == null)
                    return ApiResponse<UserDto>.NotFound("User not found");

                var user = new User
                {
                    Id = id,
                    Username = dto.Username,
                    Password = dto.Password,
                    RoleId = dto.RoleId,
                    RestaurantId = dto.RestaurantId,
                    IsActive = dto.IsActive
                };

                await _userRepository.UpdateUser(user);

                return ApiResponse<UserDto>.SuccessNoData("User updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<UserDto>> DeleteUser(int id)
        {
            try
            {
                var existing = await _userRepository.GetUserById(id);
                if (existing == null)
                    return ApiResponse<UserDto>.NotFound("User not found");

                await _userRepository.DeleteUser(id);

                return ApiResponse<UserDto>.SuccessNoData("User deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserDto>.Error(ex.Message);
            }
        }
    }
}
