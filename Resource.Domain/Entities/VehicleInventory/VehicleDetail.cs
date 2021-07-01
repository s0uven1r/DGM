namespace Resource.Domain.Entities.VehicleInventory
{
    public class VehicleDetail : BaseEntity
    {
        public string VehicleName { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public decimal Price { get; set; }
    }
}
