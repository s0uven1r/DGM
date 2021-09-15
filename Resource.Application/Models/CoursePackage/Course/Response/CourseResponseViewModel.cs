namespace Resource.Application.Models.CoursePackage.Course.Response
{
    public class CourseResponseViewModel
    {
        public string Id { get; set; }
        public string CourseName { get; set; }
        public string CourseTypeId { get; set; }
        public string CourseInfo { get; set; }
        public string RequiredDocuments { get; set; }
        public string CourseType { get; set; }
        public bool IsAdvanceCourse { get; set; }
    }
}
