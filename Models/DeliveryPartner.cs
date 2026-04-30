namespace TeaTimeDelivery.Models
{
    public class DeliveryPartner
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Phone { get; set; }
        public string VehicleType { get; set; }

        public string Location { get; set; }
        public string? Photo { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
