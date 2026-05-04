using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;
using TeaTimeDelivery.Services;

namespace TeaTimeDelivery.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
           
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var roleId = int.Parse(User.FindFirst(ClaimTypes.Role)?.Value);

            
            if (roleId != 1)
            {
                return Unauthorized("Only Admin can create orders");
            }

            
            var response = await _service.CreateOrder(dto, userId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _service.GetAllOrders();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _service.GetOrderById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("restaurant/{restaurantId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByRestaurant(int restaurantId)
        {
            var response = await _service.GetOrdersByRestaurant(restaurantId);
            return StatusCode(response.StatusCode, response);
        }
        

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _service.DeleteOrder(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("action")]
        [Authorize]
        public async Task<IActionResult> OrderAction([FromBody] OrderActionDto dto)
        {
            var response = await _service.OrderAction(dto);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{orderId}/status-history")]
        [Authorize]
        public async Task<IActionResult> GetOrderHistory(int orderId)
        {
            var result = await _service.GetOrderHistory(orderId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dashboard")]
        [Authorize]
        public async Task<IActionResult> GetDashboard(int deliveryPartnerId)
        {
            var result = await _service.GetDashboard(deliveryPartnerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{orderId}/details")]
        [Authorize]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var result = await _service.GetOrderDetails(orderId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("summary/{orderId}")]
        [Authorize]
        public async Task<IActionResult> GetOrderSummary(int orderId)
        {
            var result = await _service.GetOrderSummary(orderId);
            return StatusCode(result.StatusCode, result);

        }
        [HttpPost("sales-dashboard")]
        [Authorize]
        public async Task<IActionResult> GetSalesDashboard([FromBody] SalesDashboardRequestDto dto)
        {
            try
            {
                var response = await _service.GetSalesDashboard(dto);

                return StatusCode(response.StatusCode, response);
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<string>.Error("Internal server error"));
            }
        }
        [HttpPost("customer-summary")]
        [Authorize]
        public async Task<IActionResult> GetCustomerSummary([FromBody] CustomerSummaryRequestDto dto)
        {
            var response = await _service.GetCustomerSummary(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("shop-dashboard")]
        [Authorize]
        public async Task<IActionResult> GetShopDashboard([FromBody] ShopDashboardRequestDto dto)
        {
            var response = await _service.GetShopDashboard(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("shop-cashflow")]
        [Authorize]
        public async Task<IActionResult> GetShopCashFlow([FromBody] ShopCashflowRequestDto dto)
        {
            var response = await _service.GetShopCashFlow(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("admin-dashboard")]
        [Authorize]
        public async Task<IActionResult> GetAdminDashboard([FromBody] AdminDashboardRequestDto dto)
        {
            var response = await _service.GetAdminDashboard(dto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("AdminOrders-list")]
        [Authorize]
        public async Task<IActionResult> GetAdminOrdersList([FromBody] AdminOrdersListRequestDto dto)
        {
            var response = await _service.GetAdminOrdersList(dto);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost("AdminCash-flow")]
        [Authorize]
        public async Task<IActionResult> GetAdminCashFlow([FromBody] AdminCashFlowRequestDto dto)
        {
            var response = await _service.GetAdminCashFlow(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}