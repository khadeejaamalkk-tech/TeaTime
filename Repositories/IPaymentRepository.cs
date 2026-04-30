using TeaTimeDelivery.DTOs;

namespace TeaTimeDelivery.Repositories
{
    public interface IPaymentRepository
    {
        Task<string> CollectPayment(CollectPaymentDto dto);
    }
}
