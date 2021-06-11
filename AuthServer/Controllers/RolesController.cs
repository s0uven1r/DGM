using Auth.Infrastructure.Identity;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Models.Roles.Request;
using AuthServer.Models.Roles.Response;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{

    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ILogger<RolesController> _logger;

        public RolesController(RoleManager<AppRole> roleManager, ILogger<RolesController> logger)
        {
            _roleManager = roleManager;
            _logger = logger;
        }


        [HttpGet]
        [Route("GetRoles")]
        [ApiAuthorize(IdentityClaimConstant.ViewRole)]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(x => new GetRoleResponse
            {
                Id = x.Id,
                Name = x.Name,
                IsPublic = x.IsPublic
            });
            if (roles == null) return NotFound();
            return Ok(roles);
        }

        [HttpPost]
        [Route("AddRole")]
        [ApiAuthorize(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleRequest createRoleRequest)
        {
            var requestedBy = User.FindFirst("UserId").ToString();
            bool exists = await _roleManager.RoleExistsAsync(createRoleRequest.Name);
            if (exists)
            {
                return BadRequest($"Role \'{createRoleRequest.Name}\' is already taken.");
            }

            var role = new AppRole
            {
                Name = createRoleRequest.Name,
                IsPublic = createRoleRequest.IsPublic,
                CreatedBy = requestedBy,
                CreatedDate = DateTime.UtcNow
            };

            await _roleManager.CreateAsync(role);
            return Ok();
        }

        [HttpPut]
        [Route("UpdateRole")]
        [ApiAuthorize(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest createRoleRequest)
        {
            var requestedBy = User.FindFirst("UserId").ToString();
            var role = await _roleManager.FindByIdAsync(createRoleRequest.Id);

            if (role == null) return BadRequest($"Role \'{createRoleRequest.Name}\' not found.");
            if (role.IsDefault) return BadRequest($"Role \'{createRoleRequest.Name}\' cannot be updated.");

            role.Name = createRoleRequest.Name;
            role.IsPublic = createRoleRequest.IsPublic;
            role.LastUpdatedBy = requestedBy;
            role.LastUpdatedDate = DateTime.UtcNow;

            await _roleManager.UpdateAsync(role);
            return Ok();
        }


        [HttpDelete]
        [Route("DeleteRole/{id}")]
        [ApiAuthorize(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var requestedBy = User.FindFirst("UserId").ToString();
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Id param cannot be empty.");
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null) return BadRequest("Role not found.");
            if (role.IsDefault) return BadRequest("Role cannot be deleted.");

            await _roleManager.DeleteAsync(role);
            _logger.LogWarning("Role {1} deleted by user:{2} on {3}", role.Name, requestedBy, DateTime.UtcNow);
            return Ok();
        }
    }
}
