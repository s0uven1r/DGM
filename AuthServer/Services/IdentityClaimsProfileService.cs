using AuthServer.Entities;
using AuthServer.Persistence;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthServer.Services
{
    public class IdentityClaimsProfileService : IProfileService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _dbContext;

        public IdentityClaimsProfileService(UserManager<AppUser> userManager,
                                            RoleManager<AppRole> roleManager,
                                            AppIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var userRoles = _dbContext.UserRoles.Where(ur => ur.UserId == user.Id).FirstOrDefault();


            var roleClaims = await (from role in _dbContext.RoleClaims
                                    join claims in _dbContext.ControllerClaim
                                    on role.ClaimId equals claims.Id
                                    where role.RoleId == userRoles.RoleId
                                    select new { claims.ClaimValue, claims.Id}).ToListAsync();

            var roleDetail = await _roleManager.FindByIdAsync(userRoles.RoleId);
            var fullName = string.Join(" ", user.FirstName, user.MiddleName, user.LastName);
            var userClaims = new List<Claim>();
            userClaims.AddRange(new List<Claim>
            {

                new Claim("FullName", fullName),
                new Claim("PhoneNo", user.PhoneNumber??"-"),
                new Claim("UserId", user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Role",  roleDetail.Name),
                new Claim("RoleId",  roleDetail.Id),
                new Claim("IsKYCUpdated",  user.IsKYCUpdated.ToString()),
                 new Claim("RoleRank",  roleDetail.Rank.ToString()),
            });
            userClaims.AddRange(roleClaims.Select(a => new Claim("permission", a.ClaimValue)));
            userClaims.AddRange(roleClaims.Select(a => new Claim("permissionIds", a.Id)));
            context.IssuedClaims.Clear();
            context.IssuedClaims.AddRange(userClaims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
