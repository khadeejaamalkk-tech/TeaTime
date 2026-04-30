namespace TeaTimeDelivery.DTOs
{
    public class OrderDashboardDto
    {
        public OrderCountsDto Counts { get; set; }
        public List<PendingOrderDto> PendingOrders { get; set; }
    }
    public class OrderCountsDto
    {
        public int Pending {  get; set; }
        public int Accepted { get; set; }
        public int Completed { get; set; }
        public int Rejected { get; set; }
    }
    public class PendingOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
