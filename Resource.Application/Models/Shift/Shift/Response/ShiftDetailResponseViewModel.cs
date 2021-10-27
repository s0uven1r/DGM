namespace Resource.Application.Models.Shift.Shift.Response
{
    public class ShiftDetailResponseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string ShiftFrequencyId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Duration { get; set; }
    }
}
