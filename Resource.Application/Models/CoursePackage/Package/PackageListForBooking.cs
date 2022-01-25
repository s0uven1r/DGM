namespace Resource.Application.Models.CoursePackage.Package
{
    public class PackageListForBooking
    {
        public string PackageId { get; set; }
        public string PackageName { get; set; }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Duration { get; set; }
        public string Time { get; set; }
    }
}
