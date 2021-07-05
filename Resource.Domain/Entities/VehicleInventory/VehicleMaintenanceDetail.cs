using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Domain.Entities.VehicleInventory
{
    public class VehicleMaintenanceDetail : BaseEntity
    {
        public string VehicleId { get; set; }
        public string TypeId { get; set; }
        public string Remark { get; set; }
        public virtual VehicleDetail Vehicle { get; set; }
    }
}
