using AuthServer.Entities;
using Dgm.Common.Constants.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Persistence.Seed
{
    public static class RoleData
    {
        public static async Task SeedDefaultRolesAsync(RoleManager<AppRole> rm)
        {
            var existingRoles = rm.Roles.ToList();

            //Role Ids must be same as their Designation Ids
            var roles = RoleConstants.GetAll().Select(
                d => new AppRole
                {
                    Name = d.Title,
                    Id = d.Id,
                    IsDefault = d.IsDefault,
                    Rank = d.Rank
                }).ToList();

            foreach (var role in roles.Where(r => !existingRoles.Any(er => er.Id == r.Id)))
            {
                await rm.CreateAsync(role);
            }
        }
    }
}
