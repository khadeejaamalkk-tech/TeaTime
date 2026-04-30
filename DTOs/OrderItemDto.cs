namespace TeaTimeDelivery.DTOs
{
    public class OrderItemDto
    {
        public int Id { get; set; }           
        public int OrderId { get; set; }      
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public int? MenuId { get; set; }
    }
}
