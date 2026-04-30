using Dapper;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly DapperContext _context;
        public RoleRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateRole(Role role)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_CreateRole",
                new
                {
                    role.RoleName,
                    role.IsActive
                },
                commandType: System.Data.CommandType.StoredProcedure);
            
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Role>(
                "sp_GetAllRoles",
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
