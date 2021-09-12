using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resource.Domain.Entities.PackageCourse
{
    public class CourseType : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
    public class Course : BaseEntity
    {
        public string CourseName { get; set; }
        public string CourseTypeId { get; set; }
        public string CourseInfo { get; set; }
        public string RequiredDocuments { get; set; }
        public bool IsAdvanceCourse { get; set; }
        public virtual CourseType CourseType { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
