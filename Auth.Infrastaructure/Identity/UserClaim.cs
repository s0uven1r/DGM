using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Identity
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public string ClaimId { get; set; }
        public virtual ControllerClaim Claim { get; set; }
    }
}
