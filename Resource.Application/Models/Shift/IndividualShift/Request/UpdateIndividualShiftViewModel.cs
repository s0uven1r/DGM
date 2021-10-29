using System.Text.Json.Serialization;

namespace Resource.Application.Models.Shift.IndividualShift.Request
{
    public class UpdateIndividualShiftViewModel
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string ShiftId { get; set; }
        public string VehicleId { get; set; }
        public string TrainerId { get; set; }
        public string TrainingDate { get; set; }
        public string TrainingDateNp { get; set; }
    }
}
