using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Models.VehicleInventory.Request
{
    public class VehicleMaintenanceDetailCreateViewModel
    {
        [Required]
        public string VehicleId { get; set; }
        public string Remark { get; set; }
        public string TypeId { get; set; }
        public string RegisterDateNP { get; set; }
        public string RegisterDateEN { get; set; }
        public string UserId { get; set; }

    }
}
