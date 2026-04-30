using TeaTimeDelivery.Models;
namespace TeaTimeDelivery.Repositories
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurants();
        Task<Restaurant> GetRestaurantById(int id);
        Task CreateRestaurant(Restaurant restaurant);
        Task UpdateRestaurant(Restaurant restaurant);
        Task DeleteRestaurant(int id);

    }
}
