using System;
using System.Text.Json.Serialization;

namespace Resource.Application.Models.CoursePackage.Promo.Request
{
    public class PromoUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string PromoCode { get; set; }
        public string PackageId { get; set; }
        public bool HasDiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public string StartDate { get; set; }
        public string StartDateNp { get; set; }
        public string EndDate { get; set; }
        public string EndDateNp { get; set; }
    }
}
