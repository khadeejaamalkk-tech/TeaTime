namespace TeaTimeDelivery.DTOs
{
    public class CustomerSummaryRequestDto
    {
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public int? DeliveryPartnerId { get; set; }
    }
}
