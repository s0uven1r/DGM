using Auth.Infrastructure.Identity;
using Dgm.Common.Constants.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence.Seed
{
    public class RoleData
    {
        public static async Task SeedDefaultRolesAsync(RoleManager<AppRole> rm)
        {
            var existingRoles = rm.Roles.ToList();

            //Role Ids must be same as their Designation Ids
            var roles = RoleConstants.GetAll().Select(
                d => new AppRole
                {
                    Name = d.Title,
                    Id = d.Id
                }).ToList();

            foreach (var role in roles.Where(r => !existingRoles.Any(er => er.Id == r.Id)))
            {
                await rm.CreateAsync(role);
            }
        }
    }
}
