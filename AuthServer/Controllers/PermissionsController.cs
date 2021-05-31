﻿using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Models.Permission;
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
        //[ApiAuthorize(IdentityClaimConstant.ViewPermission)]
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
        //[ApiAuthorize(IdentityClaimConstant.UpdatePermission)]
        public async Task<IActionResult> Manage([FromBody] PermissionManagementViewModel model)
        {
            var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role == null) throw new AppException("Invalid! Role not found");

                var claimsPermissionToAdd = new List<RoleClaim>();
                var claimsPermissionToRemove = new List<RoleClaim>();
              
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
