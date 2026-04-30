namespace TeaTimeDelivery.DTOs
{
    public class UpdateDeliveryPartnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string VehicleType { get; set; }
        public string Location { get; set; }
        public IFormFile Photo { get; set; }
        public bool IsActive { get; set; }

    }
}
