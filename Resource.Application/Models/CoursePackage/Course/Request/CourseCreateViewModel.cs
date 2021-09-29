namespace Resource.Application.Models.CoursePackage.Course.Request
{
    public class CourseCreateViewModel
    {
        public string CourseName { get; set; }
        public string CourseTypeId { get; set; }
        public string CourseInfo { get; set; }
        public string RequiredDocuments { get; set; }
        public bool IsAdvanceCourse { get; set; }
    }
}
