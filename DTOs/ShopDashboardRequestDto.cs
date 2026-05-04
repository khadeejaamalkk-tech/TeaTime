namespace TeaTimeDelivery.DTOs
{
    public class ShopDashboardRequestDto
    {
        public DateTime Date { get; set; }
        public int RestaurantId { get; set; }
    }

    public class ShopOrderSummaryDto
    {
        public int PendingOrders { get; set; }
        public int AcceptedOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CancelledOrders { get; set; }
    }

    public class ShopRecentOrderDto
    {
        public string CustomerName { get; set; }
        public int OrderCount { get; set; }
        public decimal Amount { get; set; }
        public string DeliveryPartnerName { get; set; }
    }

    public class ShopDashboardResponseDto
    {
        public ShopOrderSummaryDto Summary { get; set; }
        public List<ShopRecentOrderDto> RecentOrders { get; set; }
    }
}
