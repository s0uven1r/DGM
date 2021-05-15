using Auth.Infrastructure.Identity;
using AuthServer.Models.Roles.Request;
using AuthServer.Models.Roles.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{

    [Route("Authorization/[controller]")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [HttpGet]
        [Route("GetRoles")]
        public IActionResult GetRoles()
        {
            var users = _roleManager.Roles.Select(x => new CreateRoleResponse
            {
                Id = x.Id,
                Name = x.Name
            });
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> AddRole(CreateRoleRequest createRoleRequest)
        {
            bool exists = await _roleManager.RoleExistsAsync(createRoleRequest.Name);
            if (exists)
            {
                return BadRequest($"Role \'{createRoleRequest.Name}\' is already taken.");
            }

            var role = new AppRole();
            role.Name = createRoleRequest.Name;

            await _roleManager.CreateAsync(role);
            return Ok();
        }
    }
}
