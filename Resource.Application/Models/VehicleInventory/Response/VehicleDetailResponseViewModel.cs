using System;

namespace Resource.Application.Models.VehicleInventory.Response
{
    public class VehicleDetailResponseViewModel
    {
        public string Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string ChasisNumber { get; set; }
        public string EngineNumber { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string Capacity { get; set; }
        public string ManufacturedYear { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
