using System.Text.Json.Serialization;

namespace Resource.Application.Models.Shift.ShiftFrequency.Request
{
    public class ShiftFrequencyUpdateViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
