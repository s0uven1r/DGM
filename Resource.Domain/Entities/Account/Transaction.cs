using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Domain.Entities.Account
{
    public class Transaction : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDateNP { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string Remarks { get; set; }

        public virtual ICollection<TransactionDetail> TransactionDetails { get; set; }
    }
}
