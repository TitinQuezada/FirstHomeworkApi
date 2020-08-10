using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistence.Configurations
{
    public sealed class UserRolesConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(role => role.Description).IsRequired().HasMaxLength(30);

            builder.HasData(
                new UserRole { UserRoleId = 1, Description = "Admisnistrator" },
                new UserRole { UserRoleId = 2, Description = "Client" }
                );
        }
    }
}
