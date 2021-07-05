using System.Collections.Generic;

namespace Resource.Domain.Entities.VehicleInventory
{
    public class VehicleDetail : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public string ChasisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string Capacity { get; set; }
        public string ManufacturedYear { get; set; }
        public virtual ICollection<VehicleMaintenanceDetail> VehicleMaintenance { get; set; }
    }
}
