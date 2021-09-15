using System.Text.Json.Serialization;

namespace Resource.Application.Models.CoursePackage.CourseType.Request
{
    public class CourseTypeUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
