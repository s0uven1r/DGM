using Auth.Infrastructure.Constants;
using AuthServer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Seed
{
    public static class SeedUsers
    {
        public static async Task SeedDefaultUsersAsync(UserManager<AppUser> userManager)
        {
            var userList = new List<AppUser> {
              new AppUser{
                    FirstName = "superadmin",
                    LastName = "superadmin",
                    Email = "superadmin@dgm.com",
                    UserName = "superadmin",
                    LockoutEnabled = false,
                    EmailConfirmed = true
               },
                    new AppUser{
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@dgm.com",
                    UserName = "admin",
                    LockoutEnabled = true,
                    EmailConfirmed = false
               },
              new AppUser{
                    FirstName = "consumer",
                    LastName = "consumer",
                    Email = "consumer@dgm.com",
                    UserName = "consumer",
                    LockoutEnabled = true,
                    EmailConfirmed = false
            }
        };
            foreach (var user in userList)
            {
                var result = userManager.CreateAsync(user, "Password1").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    string role = SystemRoles.Consumer;
                    if (user.UserName == "superadmin") role = SystemRoles.SuperAdmin;
                    if (user.UserName == "admin") role = SystemRoles.Admin;

                    await userManager.AddToRoleAsync(user, role);
                }
            }

        }
    }
}
