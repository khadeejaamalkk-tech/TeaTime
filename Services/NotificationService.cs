using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;

namespace TeaTimeDelivery.Services
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepo;

        public NotificationService(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }
        public async Task<ApiResponse<string>> CreateNotification(int deliveryPartnerId, string message)
        {
            try
            {
                await _notificationRepo.CreateNotification(deliveryPartnerId, message);
                return ApiResponse<string>.Success("Notification created");
            }
            catch (Exception ex)
            {
                return ApiResponse<string>.Error(ex.Message);
            }
        }
        public async Task<ApiResponse<IEnumerable<NotificationDto>>> GetNotifications(int deliveryPartnerId)
        {
            try
            {
                var data = await _notificationRepo.GetNotificationsByPartner(deliveryPartnerId);
                return ApiResponse<IEnumerable<NotificationDto>>.Success(data);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<NotificationDto>>.Error(ex.Message);
            }
        }


    }

}
