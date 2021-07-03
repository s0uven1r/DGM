using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Resource.Application.Models.VehicleInventory.Request
{
    public class VehicleDetailUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }

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
        public decimal Price { get; set; }
    }
}
