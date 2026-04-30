namespace TeaTimeDelivery.DTOs
{
    public class CreateOrderDto
    {
        public string InvoiceNo { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string Location { get; set; }

        public decimal DeliveryCharge { get; set; }
        public decimal GST { get; set; }
        public decimal Discount { get; set; }

        public int RestaurantId { get; set; }
        public string VehicleType { get; set; }
        public List<CreateOrderItemDto> Items { get; set; } = new();
    }
}
