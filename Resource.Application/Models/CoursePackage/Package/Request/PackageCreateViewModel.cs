namespace Resource.Application.Models.CoursePackage.Package.Request
{
    public class PackageCreateViewModel
    {
        public string PackageName { get; set; }
        public string CourseId { get; set; }
        public int TotalDay { get; set; }
        public string ShiftFrequencyId { get; set; }
        public decimal Price { get; set; }
    }
}
