using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Models
{
    public class CustomerPackageViewModel
    {
        public string RoleType { get; set; }
        public string AccountNo { get; set; }
        public string RoleAlias { get; set; }
        public string PackageId { get; set; }
        public string StartDate { get; set; }
        public string StartDateNP { get; set; }
        public string EndDate { get; set; }
        public string EndDateNP { get; set; }
        public string PromoCode { get; set; }
        public int PaymentGateway { get; set; }
        public string ShiftId { get; set; }
        public decimal PaidAmount { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
