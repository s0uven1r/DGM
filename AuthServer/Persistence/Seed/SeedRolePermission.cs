using Auth.Infrastructure.Constants;
using AuthServer.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Seed
{
    public class SeedRolePermission
    {
        public static async Task SeedRolewisePermission(RoleManager<AppRole> roleManager, AppIdentityDbContext dbContext)
        {
            await SuperAdminPermission(roleManager, dbContext);
        }

        private static async Task SuperAdminPermission(RoleManager<AppRole> roleManager, AppIdentityDbContext dbContext)
        {
            var roleId = (await roleManager.FindByNameAsync(SystemRoles.SuperAdmin)).Id;
            var roleClaims = dbContext.RoleClaims.Where(q => q.RoleId == roleId).Select(q => q.ClaimId);

            var claims = new List<RoleClaim>
            {
                new RoleClaim("7f51ab29-844a-470e-9172-42cde237dad9", roleId), //For view Identity
            };

            await dbContext.RoleClaims.AddRangeAsync(claims.Where(q => !roleClaims.Contains(q.ClaimId)));
            await dbContext.SaveChangesAsync();
        }
    }
}
