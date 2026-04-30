using Microsoft.AspNetCore.Mvc;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Services;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

   
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmPayment([FromBody] CollectPaymentDto dto)
    {
        if (dto == null)
            return BadRequest("Invalid request");

        var result = await _paymentService.CollectPayment(dto);

        return StatusCode(result.StatusCode, result);
    }
}