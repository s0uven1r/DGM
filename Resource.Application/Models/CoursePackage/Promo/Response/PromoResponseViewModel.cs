using System;

namespace Resource.Application.Models.CoursePackage.Promo.Response
{
    public class PromoResponseViewModel
    {
        public string Id { get; set; }
        public string PromoCode { get; set; }
        public string PackageName { get; set; }
        public string PackageId { get; set; }
        public bool HasDiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateNp { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateNp { get; set; }
    }
}
