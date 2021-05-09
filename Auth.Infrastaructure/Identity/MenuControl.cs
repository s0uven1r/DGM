using System.Collections.Generic;

namespace Auth.Infrastructure.Identity
{
    public class MenuControl
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Class { get; set; }
        public string FaClass { get; set; }
        public string RouteUrl { get; set; }
        public string ClaimId { get; set; }
        public string ParentId { get; set; }
        public int Rank { get; set; }
        public bool IsActive { get; set; }
        public virtual ControllerClaim Claim { get; set; }
        public virtual MenuControl Parent { get; set; }
        public virtual ICollection<MenuControl> Children { get; set; }
    }
}
