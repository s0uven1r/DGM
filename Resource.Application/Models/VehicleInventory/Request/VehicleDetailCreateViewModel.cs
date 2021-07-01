namespace Resource.Application.Models.VehicleInventory.Request
{
    public class VehicleDetailCreateViewModel
    {
        public string VehicleName { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public decimal Price { get; set; }
    }
}
