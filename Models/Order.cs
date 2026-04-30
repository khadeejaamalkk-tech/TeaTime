namespace TeaTimeDelivery.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string DeliveryAddress { get; set; }
        public string Location {  get; set; }

        
        public string Status { get; set; }

        
        public decimal TotalBill { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal GST { get; set; }
        public decimal Discount { get; set; }
        public decimal BillAmount { get; set; }
        public decimal TotalAmount { get; set; }

       
        public string PaymentMethod { get; set; }

        public int RestaurantId { get; set; }

     
        public string VehicleType { get; set; }

        public DateTime CreatedAt { get; set; }

        
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
