using System;
using System.ComponentModel.DataAnnotations;

namespace Resource.Application.Models.Shift.Shift.Response
{
    public class ShiftDetailResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}
