using Microsoft.AspNetCore.Identity;

namespace AuthServer.Entities
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public string ClaimId { get; set; }
        public virtual ControllerClaim Claim { get; set; }
    }
}
