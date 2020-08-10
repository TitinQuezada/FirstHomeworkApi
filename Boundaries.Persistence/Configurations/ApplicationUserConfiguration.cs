using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistence.Configurations
{
    public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(user => user.IdentificationNumber);
            builder.Property(user => user.IdentificationNumber).HasMaxLength(11);
            builder.Property(user => user.Name).IsRequired().HasMaxLength(50);
            builder.Property(user => user.Lastname).IsRequired().HasMaxLength(50);
            builder.HasIndex(user => user.Email).IsUnique();
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(50);
        }
    }
}
