using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistence.Configurations
{
    class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.Property(addres => addres.Address).IsRequired().HasMaxLength(100);
            builder.Property(addres => addres.Sector).IsRequired().HasMaxLength(30);
        }
    }
}
