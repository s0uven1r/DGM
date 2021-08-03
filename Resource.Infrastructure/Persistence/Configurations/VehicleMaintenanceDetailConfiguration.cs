using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Resource.Domain.Entities.VehicleInventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Infrastructure.Persistence.Configurations
{
    public class VehicleMaintenanceDetailConfiguration : IEntityTypeConfiguration<VehicleMaintenanceDetail>
    {
        public void Configure(EntityTypeBuilder<VehicleMaintenanceDetail> builder)
        {
            builder.HasOne(x => x.Vehicle)
         .WithMany(y => y.VehicleMaintenance)
         .HasForeignKey(a => a.VehicleId);

        }
    }
}
