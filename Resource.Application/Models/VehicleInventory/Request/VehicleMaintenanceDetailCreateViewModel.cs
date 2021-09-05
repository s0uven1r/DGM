using System.ComponentModel.DataAnnotations;

namespace Resource.Application.Models.VehicleInventory.Request
{
    public class VehicleMaintenanceDetailCreateViewModel
    {
        [Required]
        public string VehicleId { get; set; }
        public string Remark { get; set; }
        public string RegisterDateNP { get; set; }
        public string RegisterDateEN { get; set; }

    }
}
