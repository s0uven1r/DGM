using System;
using System.ComponentModel.DataAnnotations;

namespace Resource.Domain.Entities.Shift
{
    public class Shift : BaseEntity
    {
        public string Name { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }
        public virtual ShiftFrequency ShiftFrequency { get; set; }
    }
}
