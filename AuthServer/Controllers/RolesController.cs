using AuthServer.Entities;
using AuthServer.Models.Roles.Request;
using AuthServer.Models.Roles.Response;
using Dgm.Common.Attributes;
using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{

    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetRoles")]
        [Permission(IdentityClaimConstant.ViewRole)]
        public IActionResult GetRoles()
        {
            int rank = Convert.ToInt32(User.Claims.Where(x => x.Type == "RoleRank").FirstOrDefault().Value);
            var roles = _roleManager.Roles.Where(x => x.Rank > rank).Select(x => new GetRoleResponse
            {
                Id = x.Id,
                Name = x.Name,
                IsPublic = x.IsPublic,
                IsDefault = x.IsDefault,
                Rank = x.Rank,
                TypeName = x.Type == 0 ? "-" : ((RoleTypeEnum)x.Type).ToString(),
                Type = x.Type
            }).ToList();
            return Ok(roles);
        }

        [HttpPost]
        [Route("AddRole")]
        [Permission(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleRequest createRoleRequest)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            bool exists = await _roleManager.RoleExistsAsync(createRoleRequest.Name);
            if (exists)
            {
                return BadRequest($"Role \'{createRoleRequest.Name}\' is already taken.");
            }

            if (createRoleRequest.Rank == 0) return BadRequest("Invalid role rank.");

            var role = new AppRole
            {
                Name = createRoleRequest.Name,
                Rank = createRoleRequest.Rank,
                Type = createRoleRequest.Type,
                CreatedBy = requestedBy,
                CreatedDate = DateTime.UtcNow
            };

            await _roleManager.CreateAsync(role);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateRole")]
        [Permission(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest createRoleRequest)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            var role = await _roleManager.FindByIdAsync(createRoleRequest.Id);

            if (role == null) return BadRequest($"Role \'{createRoleRequest.Name}\' not found.");
            if (role.IsDefault) return BadRequest($"Role \'{createRoleRequest.Name}\' cannot be updated.");
            if (createRoleRequest.Rank == 0) return BadRequest("Invalid role rank.");

            role.Name = createRoleRequest.Name;
            role.Rank = createRoleRequest.Rank;
            role.Type = createRoleRequest.Type;
            role.LastUpdatedBy = requestedBy;
            role.LastUpdatedDate = DateTime.UtcNow;

            await _roleManager.UpdateAsync(role);
            return Ok();
        }

        [HttpGet]
        [Route("SetPublic/{roleId}")]
        [Permission(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> SetPublic(string roleId)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            var publicRoleExists = _roleManager.Roles.Where(x => x.IsPublic).FirstOrDefault() != null;

            if (publicRoleExists) return BadRequest("There is already a public role.");

            var role = _roleManager.Roles.Where(y => y.Id == roleId).FirstOrDefault();

            if (role == null) return BadRequest("Role not found.");
            if (role.IsDefault) return BadRequest("Role cannot be modified.");

            role.IsPublic = true;
            await _roleManager.UpdateAsync(role);

            return Ok();

        }

        [HttpGet]
        [Route("RemovePublic/{roleId}")]
        [Permission(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> RemovePublic(string roleId)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();

            var role = _roleManager.Roles.Where(y => y.Id == roleId).FirstOrDefault();
            if (role == null) return BadRequest("Role not found.");

            if (role.IsDefault) return BadRequest("Role cannot be modified.");
            if (!role.IsPublic) return BadRequest("This role is not Public.");

            role.IsPublic = false;
            await _roleManager.UpdateAsync(role);

            return Ok();

        }

        [HttpDelete]
        [Route("DeleteRole/{id}")]
        [Permission(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            if (string.IsNullOrEmpty(id)) return BadRequest("Role id cannot be empty.");

            var role = await _roleManager.FindByIdAsync(id);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users.Count > 0) return BadRequest("Role is assigned to one or more users.");
            if (role == null) return BadRequest("Role not found.");
            if (role.IsDefault) return BadRequest("Role cannot be deleted.");

            await _roleManager.DeleteAsync(role);
            _logger.LogWarning("Role {1} deleted by user:{2} on {3}", role.Name, requestedBy, DateTime.UtcNow);
            return Ok();
        }

        [HttpGet("Get/GetRoleTypeEnumDDL")]
        public IActionResult GetAccountTypeEnumDDL()
        {
            return Ok(RoleTypeEnumConversion.GetEnumList());
        }
    }
}
