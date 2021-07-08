using Microsoft.AspNetCore.Identity;

namespace AuthServer.Entities
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public string ClaimId { get; set; }
        public virtual ControllerClaim Claim { get; set; }

        public RoleClaim() { }
        public RoleClaim(string claimId, string roleId)
        {
            ClaimId = claimId;
            RoleId = roleId;
        }
    }
}
