using Microsoft.EntityFrameworkCore;
using Resource.Domain.Entities.Account;
using Resource.Domain.Entities.PackageCourse;
using Resource.Domain.Entities.VehicleInventory;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Resource.Application.Common.Interfaces
{
    public interface IAppDbContext : IDisposable
    {
        DbContext Instance { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesForSeedAsync(CancellationToken cancellationToken);


        DbSet<VehicleDetail> VehicleDetails { get; set; }
        DbSet<VehicleMaintenanceDetail> VehicleMaintenaceDetails { get; set; }
        DbSet<AccountType> AccountTypes { get; set; }
        DbSet<AccountHead> AccountHeads { get; set; }

        public DbSet<ClosingBalance> ClosingBalances { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackagePromoOffer> PackagePromoOffers { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
    }
}
