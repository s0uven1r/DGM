using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        // Add additional profile data for application users by adding properties to this class
        public string Name { get; set; }
    }
}
