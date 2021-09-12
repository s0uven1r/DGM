namespace Resource.Domain.Entities.VehicleInventory
{
    public class VehicleMaintenanceDetail : BaseEntity
    {
        public string VehicleId { get; set; }
        public string Remark { get; set; }
        public string Manufacturer { get; set; }
        public string RegisterDateNP { get; set; }
        public string RegisterDateEN { get; set; }
        public virtual VehicleDetail Vehicle { get; set; }
    }
}
