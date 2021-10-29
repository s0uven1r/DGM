using System;

namespace Resource.Domain.Entities.Account
{
    public class TransactionDetail : BaseEntity
    {
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionDateNP { get; set; }
        public decimal AmountDebit { get; set; }
        public decimal AmountCredit { get; set; }
        public string ReferenceAccountNumber { get; set; }
        public string Remarks { get; set; }

        public string TransactionId { get; set; }
        public virtual Transaction Transaction { get; set; }

    }
}
