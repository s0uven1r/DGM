using Microsoft.AspNetCore.Identity;
using System;

namespace AuthServer.Entities
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string CreatedBy { get; set; }
        public string AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public string LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }

        public bool IsDisabled { get; set; }
        public bool IsKYCUpdated { get; set; }
    }
}
