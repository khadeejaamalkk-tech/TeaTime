namespace TeaTimeDelivery.DTOs
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string RestaurantName { get; set; }
        public string Location { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemSummaryDto> Items { get; set; }
    }
    public class OrderItemSummaryDto
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
