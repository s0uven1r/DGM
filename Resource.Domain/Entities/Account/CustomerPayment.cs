using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Domain.Entities.Account
{
    public class CustomerPayment: BaseEntity
    {
        public decimal PackageAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsDiscountAvail { get; set; }
        public bool IsPercentDiscount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
       
    }
}
