namespace TeaTimeDelivery.DTOs
{
    public class ShopCashflowRequestDto
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int RestaurantId { get; set; }
    }
    public class ShopCashSummaryDto
    {
        public decimal TotalCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal GrandTotal { get; set; }
    }

    public class ShopPartnerCashDto
    {
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string Status { get; set; }

        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class ShopCashflowResponsetDto
    {
        public ShopCashSummaryDto Summary { get; set; }
        public List<ShopPartnerCashDto> Partners { get; set; } 
    }

}
