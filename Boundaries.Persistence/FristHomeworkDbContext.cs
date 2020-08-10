using Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace Boundaries.Persistence
{
    public sealed class FristHomeworkDbContext:DbContext
    {
        public FristHomeworkDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        public DbSet<UserPhone> UserPhones { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            int maxLength = 300;
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(FristHomeworkDbContext)));
          
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                if (!property.GetMaxLength().HasValue && property.GetColumnType() != "ntext")
                {
                    property.SetMaxLength(maxLength);
                }
                else if (property.GetColumnType() != "ntext")
                {
                     property.SetColumnType($"varchar({property.GetMaxLength().Value})");
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
