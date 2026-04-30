using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Repositories;
using TeaTimeDelivery.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<ApiResponse<string>> CollectPayment(CollectPaymentDto dto)
    {
        try
        {
            if (dto.CashAmount < 0 || dto.BankAmount < 0)
                return ApiResponse<string>.BadRequest("Invalid amount");

            if (dto.CashAmount + dto.BankAmount == 0)
                return ApiResponse<string>.BadRequest("Amount cannot be zero");

            var result = await _paymentRepository.CollectPayment(dto);

            if (result == "Already Collected")
                return ApiResponse<string>.BadRequest(result);

            return ApiResponse<string>.Success("Payment collected successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }
}