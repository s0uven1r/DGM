﻿namespace Resource.Application.Models.Shift.Shift.Request
{
    public class ShiftCreateViewModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }
        public string StartTime { get; set; }
    }
}