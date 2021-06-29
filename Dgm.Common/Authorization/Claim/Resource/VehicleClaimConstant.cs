using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.Claim.Resource
{
    public class VehicleClaimConstant
    {
        public const string ViewVehicle = "Claim.Vehicle.Read";

        // Registration
        public const string ViewRegistration = "Claim.Vehicle.Registration.Read";
        public const string WriteRegistration = "Claim.Vehicle.Registration.Write";

        // Maintenance
        public const string ViewMaintenance = "Claim.Vehicle.Maintenance.Read";
        public const string WriteMaintenance = "Claim.Vehicle.Maintenance.Write";

    }
}
