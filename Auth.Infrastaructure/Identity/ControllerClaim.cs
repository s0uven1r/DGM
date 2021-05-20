using System.Collections.Generic;

namespace Auth.Infrastructure.Identity
{
    public class ControllerClaim
    {
        public string Id { get; set; }
        public string ClaimValue { get; set; }
        public string ClaimType { get; set; }
        public string ClaimModule { get; set; }
        public string ClaimTitle { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserClaim> UserClaim { get; set; }
        public virtual ICollection<RoleClaim> RoleClaim { get; set; }
        public virtual MenuControl MenuControl { get; set; }
    }
}
