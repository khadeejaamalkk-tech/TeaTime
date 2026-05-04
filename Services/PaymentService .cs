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

            return result switch
            {
                "Success" => ApiResponse<string>.Success("Payment collected successfully"),
                "Already Collected" => ApiResponse<string>.BadRequest(result),
                "Invalid Order" => ApiResponse<string>.BadRequest(result),
                "Cash + Bank amount must equal TotalAmount" => ApiResponse<string>.BadRequest(result),
                "Invalid amount" => ApiResponse<string>.BadRequest(result),
                _ => ApiResponse<string>.Error(result)
            };
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Error(ex.Message);
        }
    }
}