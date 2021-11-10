using Auth.Infrastructure.Constants;
using AuthServer.Entities;
using AuthServer.Models.Permission;
using AuthServer.Persistence;
using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim;
using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _dbContext;
        public PermissionsController(RoleManager<AppRole> roleManager, AppIdentityDbContext dbContext)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("CheckPermission")]
        public IActionResult CheckPermission([FromBody] List<string> permissionList)
        {
            bool hasPermission = false;

            if (User.IsInRole(SystemRoles.SuperAdmin)) return Ok(true);

            var userClaims = User.Claims
                                .Where(x => x.Type == ClaimType.Permission)
                                .Select(a => a.Value).ToList();
            if (userClaims.Count > 0)
            {
                bool isAuth = userClaims.Any(x => permissionList.Contains(x));
                if (isAuth) hasPermission = true;
            }
            return Ok(hasPermission);
        }

        [HttpGet]
        [Route("GetRolePermission/{roleId}")]
        [Permission(IdentityClaimConstant.ViewPermission)]
        public async Task<IActionResult> Get(string roleId)
        {
            var existingRole = await _roleManager.FindByIdAsync(roleId);
            if (existingRole == null) throw new AppException("Invalid! Role not found");

            var existingClaims = await _dbContext.RoleClaims.Where(q => q.RoleId == roleId).Select(x => x.ClaimId).ToListAsync();
            var groupedPermissions = ClaimConstant.GetGroupedResult();
            RolePermissionViewModel rolePermission = new();
            List<RolePermissionGroup> permissions = new();
            foreach (var item in groupedPermissions)
            {
                permissions.Add(new RolePermissionGroup
                {
                    Module = item.Key,
                    PermissionList = item.OrderBy(q => q.Rank).Select(r => new PermissionList
                    {
                        ClaimId = r.Id,
                        ClaimValue = r.ClaimValue,
                        ClaimTitle = r.ClaimTitle,
                        HasClaim = existingClaims.Contains(r.Id)
                    }).ToList()
                });
            }
            rolePermission.RoleId = existingRole.Id;
            rolePermission.RoleName = existingRole.Name;
            rolePermission.RolePermissionGroup = permissions;
            return Ok(rolePermission);
        }

        [HttpPost]
        [Route("ManageRolePermission")]
        [Permission(IdentityClaimConstant.WritePermission)]
        public async Task<IActionResult> Manage([FromBody] PermissionManagementViewModel model)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null) throw new AppException("Invalid! Role not found");
                var claimsPermissionToAdd = new List<RoleClaim>();
                var claimsPermissionToRemove = new List<RoleClaim>();
                foreach (var claims in model.ClaimList)
                {
                    claimsPermissionToAdd.Add(new RoleClaim
                    {
                        ClaimId = claims.Id,
                        RoleId = role.Id
                    });
                }

                var existingClaims = await _dbContext.RoleClaims.Where(q => q.RoleId == model.RoleId).ToListAsync();
                claimsPermissionToRemove = existingClaims.Where(x => !claimsPermissionToAdd.Any(r => r.ClaimId == x.ClaimId)).ToList();
                if (claimsPermissionToRemove.Count > 0) _dbContext.RoleClaims.RemoveRange(claimsPermissionToRemove);

                claimsPermissionToAdd.RemoveAll(x => existingClaims.Any(r => r.ClaimId == x.ClaimId));
                if (claimsPermissionToAdd.Count > 0) _dbContext.RoleClaims.AddRange(claimsPermissionToAdd);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
