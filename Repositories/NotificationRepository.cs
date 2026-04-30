using Dapper;
using System.Data;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Models;
namespace TeaTimeDelivery.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DapperContext _context;
        public NotificationRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task CreateNotification(int deliveryPartnerId, string message)
        {
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(
                "sp_CreateNotification",
                new
                {
                    DeliveryPartnerId = deliveryPartnerId,
                    Message = message
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<NotificationDto>> GetNotificationsByPartner(int deliveryPartnerId)
        {
            using var connection = _context.CreateConnection();

            var result = await connection.QueryAsync<NotificationDto>(
                "sp_GetNotificationsByPartner",
                new { DeliveryPartnerId = deliveryPartnerId },
                commandType: CommandType.StoredProcedure
            );

            return result;
        }
    }
}
