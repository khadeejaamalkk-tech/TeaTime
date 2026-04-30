using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var response = await _service.CreateOrder(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _service.GetAllOrders();
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var response = await _service.GetOrderById(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("restaurant/{restaurantId}")]
        public async Task<IActionResult> GetOrdersByRestaurant(int restaurantId)
        {
            var response = await _service.GetOrdersByRestaurant(restaurantId);
            return StatusCode(response.StatusCode, response);
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var response = await _service.DeleteOrder(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("action")]
        public async Task<IActionResult> OrderAction([FromBody] OrderActionDto dto)
        {
            var response = await _service.OrderAction(dto);
            return StatusCode(response.StatusCode, response);

        }
        [HttpGet("{orderId}/status-history")]
        public async Task<IActionResult> GetOrderHistory(int orderId)
        {
            var result = await _service.GetOrderHistory(orderId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard(int deliveryPartnerId)
        {
            var result = await _service.GetDashboard(deliveryPartnerId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{orderId}/details")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            var result = await _service.GetOrderDetails(orderId);
            return StatusCode(result.StatusCode, result);
        }
        [HttpGet("summary/{orderId}")]
        public async Task<IActionResult> GetOrderSummary(int orderId)
        {
            var result = await _service.GetOrderSummary(orderId);
            return StatusCode(result.StatusCode, result);

        }
        [HttpPost("sales-dashboard")]
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
        public async Task<IActionResult> GetCustomerSummary([FromBody] CustomerSummaryRequestDto dto)
        {
            var response = await _service.GetCustomerSummary(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("shop-dashboard")]
        public async Task<IActionResult> GetShopDashboard([FromBody] ShopDashboardRequestDto dto)
        {
            var response = await _service.GetShopDashboard(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("shop-cashflow")]
        public async Task<IActionResult> GetShopCashFlow([FromBody] ShopCashflowRequestDto dto)
        {
            var response = await _service.GetShopCashFlow(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("admin-dashboard")]
        public async Task<IActionResult> GetAdminDashboard([FromBody] AdminDashboardRequestDto dto)
        {
            var response = await _service.GetAdminDashboard(dto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("AdminOrders-list")]
        public async Task<IActionResult> GetAdminOrdersList([FromBody] AdminOrdersListRequestDto dto)
        {
            var response = await _service.GetAdminOrdersList(dto);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost("AdminCash-flow")]
        public async Task<IActionResult> GetAdminCashFlow([FromBody] AdminCashFlowRequestDto dto)
        {
            var response = await _service.GetAdminCashFlow(dto);
            return StatusCode(response.StatusCode, response);
        }
    }
}