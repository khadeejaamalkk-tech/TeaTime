using TeaTimeDelivery.DTOs;
using TeaTimeDelivery.Helpers;

namespace TeaTimeDelivery.Services
{
    public interface IOrderService
    {
        Task<ApiResponse<int>> CreateOrder(CreateOrderDto dto, int userId);
        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetAllOrders();

        Task<ApiResponse<OrderResponseDto>> GetOrderById(int orderId);

        Task<ApiResponse<IEnumerable<OrderResponseDto>>> GetOrdersByRestaurant(int restaurantId);

        Task<ApiResponse<string>> DeleteOrder(int orderId);


        Task<ApiResponse<string>> OrderAction(OrderActionDto dto);
        Task<ApiResponse<string>> CancelOrderByUser(int orderId, int userId);
        Task<ApiResponse<IEnumerable<OrderStatusHistoryDto>>> GetOrderHistory(int orderId);
        Task<ApiResponse<OrderDashboardDto>> GetDashboard(int deliveryPartnerId);
        Task<ApiResponse<OrderDetailsDto>> GetOrderDetails(int orderId);
        Task<ApiResponse<OrderSummaryDto>> GetOrderSummary(int orderId);
        Task<ApiResponse<SalesDashboardResponseDto>> GetSalesDashboard(SalesDashboardRequestDto dto);
        Task<ApiResponse<List<CustomerSummaryDto>>> GetCustomerSummary(CustomerSummaryRequestDto dto);
        Task<ApiResponse<ShopDashboardResponseDto>> GetShopDashboard(ShopDashboardRequestDto dto);

        Task<ApiResponse<ShopCashflowResponsetDto>> GetShopCashFlow(ShopCashflowRequestDto dto);
        Task<ApiResponse<AdminDashboardResponseDto>> GetAdminDashboard(AdminDashboardRequestDto dto);
        Task<ApiResponse<List<AdminOrdersListResponseDto>>> GetAdminOrdersList(AdminOrdersListRequestDto dto);
        Task<ApiResponse<AdminCashFlowResponseDto>> GetAdminCashFlow(AdminCashFlowRequestDto dto);


            }
}
