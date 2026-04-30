using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class RestaurantService:IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<ApiResponse<IEnumerable<RestaurantDto>>> GetAllRestaurants()
        {
            try
            {
                var restaurants = await _restaurantRepository.GetAllRestaurants();

                var result = restaurants.Select(r => new RestaurantDto
                {
                    RestaurantId = r.RestaurantId,
                    Name = r.Name,
                    Location = r.Location,
                    IsActive = r.IsActive
                });

                return ApiResponse<IEnumerable<RestaurantDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<RestaurantDto>>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<RestaurantDto>> GetRestaurantById(int id)
        {
            try
            {
                var restaurant = await _restaurantRepository.GetRestaurantById(id);

                if (restaurant == null)
                    return ApiResponse<RestaurantDto>.NotFound("Restaurant not found");

                var result = new RestaurantDto
                {
                    RestaurantId = restaurant.RestaurantId,
                    Name = restaurant.Name,
                    Location = restaurant.Location,
                    IsActive = restaurant.IsActive
                };

                return ApiResponse<RestaurantDto>.Success(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<RestaurantDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<RestaurantDto>> CreateRestaurant(CreateRestaurantDto dto)
        {
            try
            {
                var restaurant = new Restaurant
                {
                    Name = dto.Name,
                    Location = dto.Location,
                    IsActive = true
                };

                await _restaurantRepository.CreateRestaurant(restaurant);

                return ApiResponse<RestaurantDto>.Created(null, "Restaurant created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<RestaurantDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<RestaurantDto>> UpdateRestaurant(int id, UpdateRestaurantDto dto)
        {
            try
            {
                var existing = await _restaurantRepository.GetRestaurantById(id);
                if (existing == null)
                    return ApiResponse<RestaurantDto>.NotFound("Restaurant not found");

                var restaurant = new Restaurant
                {
                    RestaurantId = id,
                    Name = dto.Name,
                    Location = dto.Location,
                    IsActive = dto.IsActive
                };

                await _restaurantRepository.UpdateRestaurant(restaurant);

                return ApiResponse<RestaurantDto>.SuccessNoData("Restaurant updated successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<RestaurantDto>.Error(ex.Message);
            }
        }

        public async Task<ApiResponse<RestaurantDto>> DeleteRestaurant(int id)
        {
            try
            {
                var existing = await _restaurantRepository.GetRestaurantById(id);
                if (existing == null)
                    return ApiResponse<RestaurantDto>.NotFound("Restaurant not found");

                await _restaurantRepository.DeleteRestaurant(id);

                return ApiResponse<RestaurantDto>.SuccessNoData("Restaurant deleted successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse<RestaurantDto>.Error(ex.Message);
            }
        }
    }
}
