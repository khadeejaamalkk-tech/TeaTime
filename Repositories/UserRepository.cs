using Dapper;
using System.Data;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.Data;


namespace TeaTimeDelivery.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        public UserRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByUsername(string username)
        {
            using (var connection = _context.CreateConnection())
            {
                var query = "SELECT * FROM Users WHERE Username = @Username";

                return await connection.QueryFirstOrDefaultAsync<User>(
                    query,
                    new { Username = username }
                );
            }
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<User>(
                "sp_GetAllUsers",
                commandType: CommandType.StoredProcedure
                );
        }
        public async Task<User> GetUserById(int id)
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(
                "sp_GetUserById",
                new { Id = id },   
                commandType: CommandType.StoredProcedure
            );
        }
        public async Task CreateUser(User user)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_CreateUser",
                new
                {
                    user.RestaurantId,
                    user.Username,
                    user.Password,
                    user.RoleId,
                    user.IsActive
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task UpdateUser(User user)
        {
            using var connection= _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_UpdateUser",
                new
                {
                    user.Id,
                    user.RestaurantId,
                    user.Username,
                    user.Password,
                    user.RoleId,
                    user.IsActive
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task DeleteUser(int id)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_DeleteUser",
                new {Id = id},
                commandType: CommandType.StoredProcedure);
        }
    }
}
