namespace Resource.Application.Models.CoursePackage.Package.Response
{
    public class PackageResponseViewModel
    {
        public string Id { get; set; }
        public string PackageName { get; set; }
        public string CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int TotalDay { get; set; }
        public decimal Duration { get; set; }
        public decimal Price { get; set; }
        public string ShiftFrequencyId { get; set; }
    }
}
