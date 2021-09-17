using System;

namespace Resource.Application.Models.CoursePackage.Promo.Request
{
    public class PromoCreateViewModel
    {
        public string PromoCode { get; set; }
        public string PackageId { get; set; }
        public bool HasDiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateNp { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateNp { get; set; }
    }
}
