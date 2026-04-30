namespace TeaTimeDelivery.Models
{
    public class User
    {
        public int Id { get; set; }
        public int? RestaurantId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public string RestaurantName { get; set; }
    }
}
