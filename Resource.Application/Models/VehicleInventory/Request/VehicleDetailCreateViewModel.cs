using System.ComponentModel.DataAnnotations;

namespace Resource.Application.Models.VehicleInventory.Request
{
    public class VehicleDetailCreateViewModel
    {
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public string ChasisNumber { get; set; }
        [Required]
        public string EngineNumber { get; set; }
        public string Model { get; set; }
        public string SubModel { get; set; }
        public string Capacity { get; set; }
        public string ManufacturedYear { get; set; }
        public string Manufacturer { get; set; }
        public string RegisterDateNP { get; set; }
        public string RegisterDateEN { get; set; }
       
    }
}
