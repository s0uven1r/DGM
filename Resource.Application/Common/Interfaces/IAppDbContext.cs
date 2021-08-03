using Microsoft.EntityFrameworkCore;
using Resource.Domain.Entities.Account;
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


        DbSet<VehicleDetail> VehicleDetails { get; set; }
        DbSet<VehicleMaintenanceDetail> VehicleMaintenaceDetails { get; set; }
        DbSet<AccountType> AccountTypes { get; set; }
        DbSet<AccountHead> AccountHeads { get; set; }
    }
}
