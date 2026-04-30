using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface INotificationService
    {
        Task<ApiResponse<string>> CreateNotification(int deliveryPartnerId, string message);

        Task<ApiResponse<IEnumerable<NotificationDto>>> GetNotifications(int deliveryPartnerId);

    }
}
