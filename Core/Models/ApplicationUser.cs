namespace Core.Models
{
    public sealed class ApplicationUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }

        public string Lastname { get; set; }

        public string IdentificationNumber { get; set; }

        public string Email { get; set; }

        public UserPhone Phone { get; set; }

        public UserAddress Address { get; set; }

        public UserRole Role { get; set; }
    }
}
