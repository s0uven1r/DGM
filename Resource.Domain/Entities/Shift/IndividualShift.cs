using Resource.Domain.Entities.PackageCourse;
using System;

namespace Resource.Domain.Entities.Shift
{
    public class IndividualShift : BaseEntity
    {
        public string ShiftId { get; set; }
        public string PackageId { get; set; }
        public string VehicleId { get; set; }
        public string TrainerId { get; set; }
        public string TrainerDetail { get; set; }
        public DateTime? TrainingDate { get; set; }
        public string TrainingDateNp { get; set; }
        public string UserAccountNumber { get; set; }
        public virtual Package Package { get; set; }
        public virtual Shift Shift { get; set; }
    }
}
