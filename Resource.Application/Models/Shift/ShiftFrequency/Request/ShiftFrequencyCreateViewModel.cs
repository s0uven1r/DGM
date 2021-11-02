namespace Resource.Application.Models.Shift.ShiftFrequency.Request
{
    public class ShiftFrequencyCreateViewModel
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public bool IsActive { get; set; }
    }
}
