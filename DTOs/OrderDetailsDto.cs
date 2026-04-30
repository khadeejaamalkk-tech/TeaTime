namespace TeaTimeDelivery.DTOs
{
    public class OrderDetailsDto
    {
        public string InvoiceNo { get; set; }
        public string VehicleType { get; set; }
        public string MobileNo { get; set; }
        public string Location { get; set; }   // ✅ instead of Landmark
        public string CustomerName { get; set; }

        public decimal BillAmount { get; set; }
        public decimal DeliveryCharge { get; set; }
        public decimal GST { get; set; }
        public decimal TotalBill { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
