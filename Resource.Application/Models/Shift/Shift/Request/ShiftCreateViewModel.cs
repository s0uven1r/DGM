using System;
using System.ComponentModel.DataAnnotations;

namespace Resource.Application.Models.Shift.Shift.Request
{
    public class ShiftCreateViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
    }
}
