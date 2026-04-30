using Dapper;
using System.Data;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DapperContext _context;
        public RestaurantRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
            using var connection = _context.CreateConnection();
            return await  connection.QueryAsync<Restaurant>(
                "sp_GetAllRestaurants",
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task<Restaurant> GetRestaurantById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Restaurant>(
                "sp_GetRestaurantById",
                new { RestaurantId = id },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task CreateRestaurant(Restaurant restaurant)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_CreateRestaurant",
                new
                {
                    restaurant.Name,
                    restaurant.Location,
                    restaurant.IsActive
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task UpdateRestaurant(Restaurant restaurant)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_UpdateRestaurant",
                new
                {
                    restaurant.RestaurantId,
                    restaurant.Name,
                    restaurant.Location,
                    restaurant.IsActive
                },
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task DeleteRestaurant(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_DeleteRestaurant",
                new { RestaurantId = id },
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
