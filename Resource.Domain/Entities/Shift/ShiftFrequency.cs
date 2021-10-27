using Resource.Domain.Entities.PackageCourse;
using System.Collections.Generic;

namespace Resource.Domain.Entities.Shift
{
    public class ShiftFrequency : BaseEntity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Shift> Shifts { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
