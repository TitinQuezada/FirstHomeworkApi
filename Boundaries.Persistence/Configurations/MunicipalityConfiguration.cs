using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boundaries.Persistence.Configurations
{
    public sealed class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.Property(municipality => municipality.Description).IsRequired().HasMaxLength(30);

            builder.HasData(
                new Municipality { MunicipalityId = 1, Description = "Santo Domingo Norte" },
                new Municipality { MunicipalityId = 2, Description = "Santo Domingo Oeste" },
                new Municipality { MunicipalityId = 3, Description = "Santo Domingo Este" },
                new Municipality { MunicipalityId = 4, Description = "Boca Chica" },
                new Municipality { MunicipalityId = 5, Description = "Los Alcarrisos" },
                new Municipality { MunicipalityId = 6, Description = "Pedro Brand" },
                new Municipality { MunicipalityId = 7, Description = "San Antonio de Guerra" },
                new Municipality { MunicipalityId = 8, Description = "San Luis" }
                );
        }
    }
}
