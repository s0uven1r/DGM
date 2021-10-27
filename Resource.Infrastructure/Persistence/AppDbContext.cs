using Microsoft.EntityFrameworkCore;
using Resource.Application.Common.Interfaces;
using Resource.Domain.Entities;
using Resource.Domain.Entities.Account;
using Resource.Domain.Entities.PackageCourse;
using Resource.Domain.Entities.Shift;
using Resource.Domain.Entities.VehicleInventory;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IUserAccessor _currentUserService;
        private readonly IDateTime _dateTime;

        public AppDbContext(DbContextOptions options,
            IUserAccessor currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbContext Instance => this;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.CreatedDate = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        entry.Entity.UpdatedDate = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public async Task<int> SaveChangesForSeedAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                   .SelectMany(t => t.GetProperties())
                   .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("decimal(15, 4)");
            }
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        public DbSet<VehicleDetail> VehicleDetails { get; set; }
        public DbSet<VehicleMaintenanceDetail> VehicleMaintenaceDetails { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<AccountHead> AccountHeads { get; set; }
        public DbSet<AccountCountTable> AccountCountTables { get; set; }
        public DbSet<ClosingBalance> ClosingBalances { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackagePromoOffer> PackagePromoOffers { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<ShiftFrequency> ShiftFrequencies { get; set; }
        public DbSet<Shift> Shifts { get; set; }
    }
}
