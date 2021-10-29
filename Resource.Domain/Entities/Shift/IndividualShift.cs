using System;

namespace Resource.Domain.Entities.Shift
{
    public class IndividualShift : BaseEntity
    {
        public string ShiftId { get; set; }
        public string PackageId { get; set; }
        public string VehicleId { get; set; }
        public string TrainerId { get; set; }
        public DateTime TrainingDate { get; set; }
        public string UserAccountNumber { get; set; }
    }
}
