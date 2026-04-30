namespace TeaTimeDelivery.DTOs
{
    public class SalesDashboardResponseDto
    {
        public decimal TotalCash { get; set; }
        public decimal TotalCard { get; set; }
        public decimal TotalSales { get; set; }

        public List<CompletedOrderDto> CompletedOrders { get; set; }
    }
    public class CompletedOrderDto
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }
        public string BranchName { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
    }
}
