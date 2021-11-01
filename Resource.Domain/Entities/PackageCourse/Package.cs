using Resource.Domain.Entities.Shift;
using System;
using System.Collections.Generic;

namespace Resource.Domain.Entities.PackageCourse
{
    public class Package : BaseEntity
    {
        public string PackageName { get; set; }
        public string CourseId { get; set; }
        public int TotalDay { get; set; }
        public decimal Duration { get; set; }
        public decimal Price { get; set; }
        public string ShiftFrequencyId { get; set; }
        public virtual ICollection<PackagePromoOffer> PackagePromoOffers { get; set; }
        public virtual ICollection<CustomerPackage> CustomerPackages { get; set; }
        public virtual Course Course { get; set; }
        public virtual ShiftFrequency ShiftFrequency { get; set; }
    }
    public class PackagePromoOffer : BaseEntity
    {
        public string PromoCode { get; set; }
        public string PackageId { get; set; }
        public bool HasDiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateNp { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateNp { get; set; }
        public virtual Package Package { get; set; }
    }
}
