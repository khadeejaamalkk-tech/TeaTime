namespace TeaTimeDelivery.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int DeliveryPartnerId { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
