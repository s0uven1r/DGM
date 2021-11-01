using Resource.Domain.Entities.PackageCourse;

namespace Resource.Domain.Entities.Account
{
    public class CustomerPayment: BaseEntity
    {
        public string AccountNumber { get; set; }
        public string CustomerPackageId { get; set; }
        public decimal PaidAmount { get; set; }
        public bool IsDiscountAvail { get; set; }
        public bool IsPercentDiscount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int PaymentGateway { get; set; }
        public virtual CustomerPackage CustomerPackage { get; set; }

    }
}
