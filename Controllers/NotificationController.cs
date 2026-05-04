using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.Services;

namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController:ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("{deliveryPartnerId}")]
        [Authorize]
        public async Task<IActionResult> GetNotifications(int deliveryPartnerId)
        {
            var result = await _notificationService.GetNotifications(deliveryPartnerId);
            return StatusCode(result.StatusCode, result);
        }
    }
}
