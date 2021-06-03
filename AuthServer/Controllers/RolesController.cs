using Auth.Infrastructure.Identity;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Models.Roles.Request;
using AuthServer.Models.Roles.Response;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("GetRoles")]
        [ApiAuthorize(IdentityClaimConstant.ViewRole)]
        public IActionResult GetRoles()
        {
            var abc = User.Claims.ToList();
            var users = _roleManager.Roles.Select(x => new GetRoleResponse
            {
                Id = x.Id,
                Name = x.Name
            });
            if (users == null) return NotFound();
            return Ok(users);
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
                Name = createRoleRequest.Name
            };

            await _roleManager.CreateAsync(role);
            return Ok();
        }

        [HttpPost]
        [Route("UpdateRole")]
        [ApiAuthorize(IdentityClaimConstant.WriteRole)]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest createRoleRequest)
        {
            var requestedBy = User.FindFirst("UserId").ToString();
            var role = await _roleManager.FindByIdAsync(createRoleRequest.Id);
            if (role == null)
            {
                return BadRequest($"Role \'{createRoleRequest.Name}\' not found.");
            }

            role.Name = createRoleRequest.Name;
            role.LastUpdatedBy = requestedBy;
            role.LastUpdatedDate = DateTime.UtcNow;

            await _roleManager.CreateAsync(role);
            return Ok();
        }
    }
}
