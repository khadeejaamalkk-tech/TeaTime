namespace TeaTimeDelivery.DTOs
{
    public class RestaurantDto
    {
        public string ?Username { get; set; }
        public string ?Password { get; set; }
        public int RoleId { get; set; }
        public int? RestaurantId { get; set; }
        public bool IsActive { get; set; }
        public string ?Name { get; set; }        
        public string ?Location { get; set; }
    }
}
