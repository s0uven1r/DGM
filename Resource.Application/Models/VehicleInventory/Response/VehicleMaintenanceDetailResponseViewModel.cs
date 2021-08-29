﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Application.Models.VehicleInventory.Response
{
    public class VehicleMaintenanceDetailResponseViewModel
    {
        public string RegistrationNumber { get; set; }
        public string Remark { get; set; }
        public string Id { get; set; }
        public string VehicleId { get; set; }
        public string Manufacturer { get; set; }
        public string RegisterDateNP { get; set; }
        public string RegisterDateEN { get; set; }

    }
}
