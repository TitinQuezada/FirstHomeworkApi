namespace Core.Models
{
    public sealed class UserAddress
    {
        public int UserAddressId { get; set; }

        public string Address { get; set; }

        public string Sector { get; set; }

        public Municipality Municipality { get; set; }
    }
}
