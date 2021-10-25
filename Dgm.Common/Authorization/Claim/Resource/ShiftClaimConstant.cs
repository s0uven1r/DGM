using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.Claim.Resource
{
    public class ShiftClaimConstant
    {
        public const string ViewShiftManagement = "Claim.ShiftManagement.Read";

        // ShiftFrequency
        public const string ViewShiftFrequency = "Claim.ShiftManagement.ShiftFrequency.Read";
        public const string WriteShiftFrequency = "Claim.ShiftManagement.ShiftFrequency.Write";

        // Shift
        public const string ViewShift = "Claim.ShiftManagement.Shift.Read";
        public const string WriteShift = "Claim.ShiftManagement.Shift.Write"; 
        
        // Shift
        public const string ViewIndividualShift = "Claim.ShiftManagement.IndividualShift.Read";
        public const string WriteIndividualShift = "Claim.ShiftManagement.IndividualShift.Write";
    }
}
