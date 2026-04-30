using Dapper;
using System.Data;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.Repositories
{
    public class DeliveryPartnerRepository:IDeliveryPartnerRepository
    {
        private readonly DapperContext _context;
        public DeliveryPartnerRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<DeliveryPartner> CreateDeliveryPartner(DeliveryPartner partner)
        {
            using var connection = _context.CreateConnection();
            var id = await connection.ExecuteScalarAsync<int>(
                "sp_CreateDeliveryPartner",
                new
                {
                    partner.Name,
                    partner.Phone,
                    partner.VehicleType,
                    partner.Location,
                    partner.IsActive
                },
                commandType: CommandType.StoredProcedure
            );
            return new DeliveryPartner
            {
                Id = id,
                Name = partner.Name,
                Phone = partner.Phone,
                VehicleType = partner.VehicleType,
                Location = partner.Location,
                IsActive = partner.IsActive

            };
        }
        public async Task<DeliveryPartner> GetDeliveryPartnerById(int id)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DeliveryPartner>(
                "sp_GetDeliveryPartnerById",
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<DeliveryPartner>> GetAllDeliveryPartners()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DeliveryPartner>(
                "sp_GetAllDeliveryPartners",
                commandType: CommandType.StoredProcedure);
        }
        public async Task<DeliveryPartner> UpdateDeliveryPartner(UpdateDeliveryPartnerDto dto, string photoName)
        {
            using var connection= _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DeliveryPartner>(
                "sp_UpdateDeliveryPartner",
                new
                {
                    dto.Id,
                    dto.Name,
                    dto.Phone,
                    dto.VehicleType,
                    dto.Location,
                    Photo = photoName,
                    dto.IsActive

                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<bool> DeleteDeliveryPartner(int id)
        {
            using var connection = _context.CreateConnection();
            var result = await connection.ExecuteAsync(
                "sp_DeleteDeliveryPartner",
                new {Id=id},
                commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public async Task<IEnumerable<DeliveryPartner>> GetByVehicleType(string vehicleType)
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<DeliveryPartner>(
                "SELECT * FROM DeliveryPartners WHERE VehicleType = @VehicleType AND IsActive = 1",
                new { vehicleType = vehicleType });
        }
    }
}
