namespace TeaTimeDelivery.DTOs
{
    public class AdminDashboardRequestDto
    {
        public DateTime? Date { get; set; }
        public int? RestaurantId { get; set; }
    }
    public class AdminDashboardSummaryDto
    {
        public int PendingOrders { get; set; }
        public int AcceptedOrders { get; set; }
        public int CompletedOrders { get; set; }
        public int CancelledOrders { get; set; }
    }
    public class AdminDashboardRecentOrderDto
    {
        public string CustomerName { get; set; }
        public string RestaurantName { get; set; }
        public decimal Amount { get; set; }
        public string DeliveryPerson { get; set; }
        public int StatusId { get; set; }
    }
    public class AdminDashboardResponseDto
    {
        public AdminDashboardSummaryDto Summary { get; set; }
        public List<AdminDashboardRecentOrderDto> RecentOrders { get; set; }
    }
}
