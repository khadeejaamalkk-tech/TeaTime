using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface IPaymentService
    {
        Task<ApiResponse<string>> CollectPayment(CollectPaymentDto dto);

    }
}
