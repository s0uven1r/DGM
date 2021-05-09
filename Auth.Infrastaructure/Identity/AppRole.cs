using Microsoft.AspNetCore.Identity;

namespace Auth.Infrastructure.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public bool IsDefault { get; set; }
    }
}
