using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Identity
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public string ClaimId { get; set; }
        public virtual ControllerClaim Claim { get; set; }
    }
}
