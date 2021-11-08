using System;

namespace Resource.Domain.Entities.Shift
{
    public class Shift : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }
        public virtual ShiftFrequency ShiftFrequency { get; set; }
    }
}
