namespace TeaTimeDelivery.DTOs
{
    public class CustomerSummaryDto
    {
        public string CustomerName { get; set; }
        public string Location { get; set; }

        public int OrderCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class CustomerSummaryResponseDto
    {
        public List<CustomerSummaryDto> Customers { get; set; }
    }
}
