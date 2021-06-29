using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.MenuControl.Resource
{
    public class VehicleMenuConstant
    {
        public static KeyValuePair<string, string> VehicleManagement = new("Vehicle Management", "vehicle");
        public static KeyValuePair<string, string> Registration = new("Purchase", "purchase");
        public static KeyValuePair<string, string> Maintenance = new("Maintenance", "maintenance");

    }
}
