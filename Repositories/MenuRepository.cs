using System.Data;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.Models;
using TeaTimeDelivery.DTOs;
using Dapper;

namespace TeaTimeDelivery.Repositories
{
    public class MenuRepository:IMenuRepository
    {
        private readonly DapperContext _context;
        public MenuRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<MenuDto>> GetAllMenu()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<MenuDto>(
                "sp_GetAllMenu",
                commandType: CommandType.StoredProcedure);
        }
        public async Task<int> AddMenu(CreateMenuDto dto)
        {
            using var connection = _context.CreateConnection();

            var id = await connection.ExecuteScalarAsync<int>(
                "sp_AddMenu",
                new
                {
                    dto.Name,
                    dto.Price
                },
                commandType: CommandType.StoredProcedure
            );

            return id;
        }
    }
}
