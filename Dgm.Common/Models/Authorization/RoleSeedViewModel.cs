namespace Dgm.Common.Models.Authorization
{
    public class RoleSeedViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Rank { get; set; }
        public int Type { get; set; }
        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; }
    }
}
