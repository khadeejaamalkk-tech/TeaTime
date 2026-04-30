namespace TeaTimeDelivery.DTOs
{
    public class OrderStatusHistoryDto
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int? DeliveryPartnerId { get; set; } 

        public int StatusId { get; set; }

        

        public DateTime CreatedAt { get; set; }
    }
}
