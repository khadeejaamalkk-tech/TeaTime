using Dapper;
using System.Data;
using TeaTimeDelivery.Data;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly DapperContext _context;

    public PaymentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<string> CollectPayment(CollectPaymentDto dto)
    {
        using var connection = _context.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@OrderId", dto.OrderId);
        parameters.Add("@CashAmount", dto.CashAmount);
        parameters.Add("@BankAmount", dto.BankAmount);

        var result = await connection.QueryFirstOrDefaultAsync<string>(
            "sp_CollectPayment",
            parameters,
            commandType: CommandType.StoredProcedure
        );

        return result;
    }
}