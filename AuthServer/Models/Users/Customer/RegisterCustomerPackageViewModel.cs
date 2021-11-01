using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models.Users.Customer
{
    public class RegisterCustomerPackageViewModel
    {
        public RegisterViewModel CustomerDetail { get; set; }
        public string PackageId { get; set; }
        public string StartDate { get; set; }
        public string StartDateNP { get; set; }
        public string EndDate { get; set; }
        public string EndDateNP { get; set; }
        public int PaymentGateway { get; set; }
        public string ShiftId { get; set; }
        public decimal PaidAmount { get; set; }
    }
}
