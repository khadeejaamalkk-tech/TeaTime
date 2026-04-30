namespace TeaTimeDelivery.DTOs
{
    public class CollectPaymentDto
    {
        public int OrderId { get; set; }
        public decimal CashAmount { get; set; }
        public decimal BankAmount { get; set; }
        public int DeliveryPartnerId { get; set; }
    }
}
