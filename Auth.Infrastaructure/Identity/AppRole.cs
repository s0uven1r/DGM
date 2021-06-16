using Microsoft.AspNetCore.Identity;
using System;

namespace Auth.Infrastructure.Identity
{
    public class AppRole : IdentityRole<string>
    {
        public AppRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        public bool IsDefault { get; set; }
        public bool IsPublic { get; set; } //role should be public or not

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
