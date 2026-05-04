namespace TeaTimeDelivery.DTOs
{
    public class AdminOrdersListRequestDto
    {
        public DateTime? Date { get; set; }
        public int? RestaurantId { get; set; }
    }
    public class AdminOrdersListResponseDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string RestaurantName { get; set; } = string.Empty;
        public decimal TotalSpent { get; set; }
        public DateTime LastOrderDate { get; set; }
    }
}
