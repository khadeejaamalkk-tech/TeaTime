namespace TeaTimeDelivery.DTOs
{
    public class OrderActionDto
    {
        public int OrderId { get; set; }
        public int DeliveryPartnerId { get; set; }
        public string ActionType { get; set; }
        public string? OTP { get; set; }
    }
}
