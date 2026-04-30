using TeaTimeDelivery.Models;
namespace TeaTimeDelivery.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string name);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);

    }
}
