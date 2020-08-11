using Core.Models;

namespace Core.ViewModels
{
    public sealed class ApplicationUserViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Lastname { get; set; }

        public string IdentificationNumber { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string Sector { get; set; }

        public Municipality Municipality { get; set; }
    }
}
