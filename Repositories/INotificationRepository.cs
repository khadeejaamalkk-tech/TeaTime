using TeaTimeDelivery.DTOs;
namespace TeaTimeDelivery.Repositories
{
    public interface INotificationRepository
    {
        Task CreateNotification(int deliveryPartnerId, string message);
        Task<IEnumerable<NotificationDto>> GetNotificationsByPartner(int deliveryPartnerId);
    }
}
