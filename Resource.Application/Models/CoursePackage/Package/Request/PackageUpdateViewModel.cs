using System.Text.Json.Serialization;

namespace Resource.Application.Models.CoursePackage.Package.Request
{
    public class PackageUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public string CourseId { get; set; }
        public int TotalDay { get; set; }
        public string ShiftFrequencyId { get; set; }
        public decimal Price { get; set; }
    }
}
