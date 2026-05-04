using TeaTimeDelivery.Models;

namespace TeaTimeDelivery.DTOs
{
    public class AdminCashFlowRequestDto
    {
        public DateTime? Date { get; set; }
        public int? RestaurantId { get; set; }
    }
    public class AdminCashFlowSummaryDto
    {
        public decimal TotalCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal GrandTotal { get; set; }
    }
    public class RestaurantCashFlowDto
    {
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public decimal TotalCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal GrandTotal { get; set; }
    }
    public class AdminCashFlowResponseDto
    {
        public AdminCashFlowSummaryDto Summary { get; set; }
        public List<RestaurantCashFlowDto> Restaurants { get; set; }
    }
}

