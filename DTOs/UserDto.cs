namespace TeaTimeDelivery.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int? RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public bool IsActive { get; set; }
    }
}

