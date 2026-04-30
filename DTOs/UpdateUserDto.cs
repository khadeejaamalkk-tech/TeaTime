namespace TeaTimeDelivery.DTOs
{
    public class UpdateUserDto
    {
        public string ?Username { get; set; }
        public string ?Password { get; set; }
        public int RoleId { get; set; }
        public int? RestaurantId { get; set; }
        public bool IsActive { get; set; }
    }
}
