using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface IRestaurantService
    {
        Task<ApiResponse<IEnumerable<RestaurantDto>>> GetAllRestaurants();
        Task<ApiResponse<RestaurantDto>> GetRestaurantById(int id);
        Task<ApiResponse<RestaurantDto>> CreateRestaurant(CreateRestaurantDto dto);
        Task<ApiResponse<RestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto);
        Task<ApiResponse<RestaurantDto>> DeleteRestaurant(int id);
    }
}
