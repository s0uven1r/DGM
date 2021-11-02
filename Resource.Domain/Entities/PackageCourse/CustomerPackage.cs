using Resource.Domain.Entities.Account;
using System;
using System.Collections.Generic;

namespace Resource.Domain.Entities.PackageCourse
{
    public class CustomerPackage : BaseEntity
    {
        public string PackageId { get; set; }
        public string PromoCode { get; set; }
        public string CustomerAccountNumber { get; set; }
        public DateTime PackageStartDate { get; set; }
        public string PackageStartDateNp { get; set; }
        public DateTime PackageEndDate { get; set; }
        public string PackageEndDateNp { get; set; }
        public virtual Package Package { get; set; }
        public virtual ICollection<CustomerPayment> CustomerPayments { get; set; }
    }
}
