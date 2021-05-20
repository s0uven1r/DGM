using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using AuthServer.Models.Permission;
using Dgm.Common.Authorization.Claim;
using Dgm.Common.Error;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    //[Authorize(LocalApi.PolicyName)]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _dbContext;
        public PermissionsController(RoleManager<AppRole> roleManager, AppIdentityDbContext dbContext)
        {
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetRolePermission/{roleId}")]
        //[ApiAuthorize(IdentityClaimConstant.CreateIdentity)]
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
        //[ApiAuthorize(IdentityClaimConstant.CreateIdentity)]
        public async Task<IActionResult> Manage([FromBody] RolePermissionViewModel model)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null) throw new AppException("Invalid! Role not found");

                var claimsPermissionToAdd = new List<RoleClaim>();
                var claimsPermissionToRemove = new List<RoleClaim>();
                foreach (var claims in model.RolePermissionGroup)
                {
                    foreach (var item in claims.PermissionList)
                    {
                        if (item.HasClaim)
                        {
                            claimsPermissionToAdd.Add(new RoleClaim
                            {
                                ClaimId = item.ClaimId,
                                RoleId = role.Id
                            });
                        }
                    }
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
