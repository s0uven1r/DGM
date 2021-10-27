using System.Text.Json.Serialization;

namespace Resource.Application.Models.Shift.Shift.Request
{
    public class ShiftUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }
        public string StartTime { get; set; }
    }
}
