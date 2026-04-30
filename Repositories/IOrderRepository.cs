using TeaTimeDelivery.DTOs;

namespace TeaTimeDelivery.Repositories
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(CreateOrderDto order);
        Task<OrderResponseDto> GetOrderById(int id);
        Task<IEnumerable<OrderResponseDto>> GetAllOrders();
        Task<IEnumerable<OrderResponseDto>> GetOrdersByRestaurant(int restaurantId);
        Task<bool> DeleteOrder(int id);
        Task<int> OrderAction(OrderActionDto dto);
        Task<int> CancelOrderByUser(int orderId, int userId);
        Task<IEnumerable<OrderStatusHistoryDto>> GetOrderStatusHistory(int orderId);
        Task<OrderDashboardDto> GetOrderDashboard(int deliveryPartnerId, string vehicleType);
        Task<OrderDetailsDto> GetOrderDetails(int orderId);
        Task<OrderSummaryDto> GetOrderSummary(int orderId);
        Task<SalesDashboardResponseDto> GetSalesDashboard(SalesDashboardRequestDto dto);
        Task<List<CustomerSummaryDto>> GetCustomerSummary(CustomerSummaryRequestDto dto);
        Task<ShopDashboardResponseDto> GetShopDashboard(ShopDashboardRequestDto dto);
        Task<ShopCashflowResponsetDto> GetShopCashflow(ShopCashflowRequestDto dto);
        Task<AdminDashboardResponseDto> GetAdminDashboard(AdminDashboardRequestDto dto);
        Task<List<AdminOrdersListResponseDto>> GetAdminOrdersList(AdminOrdersListRequestDto dto);
        Task<AdminCashFlowResponseDto> GetAdminCashFlow(AdminCashFlowRequestDto dto);

    }
}
