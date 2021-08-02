using Microsoft.EntityFrameworkCore;
using Resource.Domain.Entities.Account;
using Resource.Domain.Entities.VehicleInventory;

namespace Resource.Domain.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<VehicleMaintenanceDetail>().HasOne(x => x.Vehicle)
            .WithMany(y => y.VehicleMaintenance)
            .HasForeignKey(a => a.VehicleId);

            builder.Entity<AccountHead>().HasOne(x => x.AccountType)
            .WithMany(y => y.AccountHeads)
            .HasForeignKey(a => a.AccountTypeId);
        }

        public DbSet<VehicleDetail> VehicleDetails { get; set; }
        public DbSet<VehicleMaintenanceDetail> VehicleMaintenaceDetails { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountHead> AccountHeads { get; set; }
    }
}
