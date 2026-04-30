namespace TeaTimeDelivery.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public int DeliveryPartnerId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
