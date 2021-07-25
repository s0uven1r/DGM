using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.Claim.Resource
{
    public class AccountingClaimConstant
    {
        public const string ViewAccounting = "Claim.Accounting.Read";

        // Accounting Type
        public const string ViewAccountingType = "Claim.Accounting.AccountingType.Read";
        public const string WriteAccountingType = "Claim.Accounting.AccountingType.Write";

        // Accounting Head
        public const string ViewAccountingHead = "Claim.Accounting.AccountingHead.Read";
        public const string WriteAccountingHead = "Claim.Accounting.AccountingHead.Write";
    }
}
