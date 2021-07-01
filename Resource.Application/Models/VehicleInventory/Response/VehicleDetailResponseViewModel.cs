using System;

namespace Resource.Application.Models.VehicleInventory.Response
{
    public class VehicleDetailResponseViewModel
    {
        public string Id { get; set; }
        public string VehicleName { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public decimal Price { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
