using Auth.Infrastructure.Constants;
using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Models.Users;
using AuthServer.Models.Users.Employee.Request;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppIdentityDbContext appIdentityDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appIdentityDbContext = appIdentityDbContext;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [ApiAuthorize(IdentityClaimConstant.ViewUser)]
        public async Task<IActionResult> GetAllUsers()
        {
            var joinResult = await (from user in _appIdentityDbContext.Users
                                    join userRole in _appIdentityDbContext.UserRoles
                                    on user.Id equals userRole.UserId
                                    join role in _appIdentityDbContext.Roles
                                    on userRole.RoleId equals role.Id
                                    select new
                                    {
                                        user,
                                        role
                                    }
                             ).ToListAsync();

            if (joinResult == null) return NotFound();

            var users = joinResult.Select(x => new GetUserResponse
            {
                Id = x.user.Id,
                Email = x.user.Email,
                UserName = x.user.UserName,
                FirstName = x.user.FirstName,
                MiddleName = x.user.MiddleName,
                LastName = x.user.LastName,
                PhoneNumber = x.user.PhoneNumber,
                RoleId = x.role.Id,
                RoleName = x.role.Name,
                IsDefault = x.role.IsDefault,
                IsEnabled = !x.user.IsDisabled
            }).ToList();


            return Ok(users);
        }

        [HttpGet]
        [Route("GetUser/{userId}")]
        [ApiAuthorize(IdentityClaimConstant.ViewUser)]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
            if (user == null) return BadRequest("User not found");

            var roleNameList = await _userManager.GetRolesAsync(user);
            var roleName = roleNameList.FirstOrDefault();
            var role = await _roleManager.Roles.Where(r => r.Name == roleName).FirstOrDefaultAsync();

            var userResult = new GetUserResponse
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                RoleId = role.Id,
                RoleName = roleName,
                IsDefault = role.IsDefault
            };
            return Ok(userResult);
        }

        [HttpPost]
        [Route("CreateEmployee")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
        {
            var requestedBy = User.FindFirst("UserId").ToString();

            bool usernameExists = await _userManager.FindByNameAsync(createEmployeeRequest.UserName) != null;
            if (usernameExists)
            {
                return BadRequest("Username is taken.");
            }

            bool emailExists = await _userManager.FindByEmailAsync(createEmployeeRequest.Email) != null;
            if (emailExists)
            {
                return BadRequest("Email is in use.");
            }

            var applicationUser = new AppUser
            {
                UserName = createEmployeeRequest.UserName,
                Email = createEmployeeRequest.Email,
                FirstName = createEmployeeRequest.FirstName,
                MiddleName = createEmployeeRequest.MiddleName,
                LastName = createEmployeeRequest.LastName,
                PhoneNumber = createEmployeeRequest.Phone,
                CreatedBy = requestedBy,
                CreatedDate = DateTime.UtcNow,
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, createEmployeeRequest.Password);

            if (!identityResult.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var roleName = (await _roleManager.FindByIdAsync(createEmployeeRequest.RoleId))?.Name;

            if (string.IsNullOrEmpty(roleName))
            {
                await _userManager.DeleteAsync(applicationUser);
                return BadRequest("Role not found.");
            }

            var roleResult = await _userManager.AddToRoleAsync(applicationUser, roleName);

            if (roleResult.Succeeded)
            {
                return Ok();
            }
            else
            {
                await _userManager.DeleteAsync(applicationUser);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest createEmployeeRequest)
        {
            var requestedBy = User.FindFirst("UserId").ToString();
            using (var transaction = _appIdentityDbContext.Database.BeginTransaction())
            {

                var user = await _userManager.FindByIdAsync(createEmployeeRequest.Id);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                user.FirstName = createEmployeeRequest.FirstName;
                user.MiddleName = createEmployeeRequest.MiddleName;
                user.LastName = createEmployeeRequest.LastName;
                user.PhoneNumber = createEmployeeRequest.Phone;
                user.LastUpdatedBy = requestedBy;
                user.LastUpdatedDate = DateTime.UtcNow;


                var identityResult = await _userManager.UpdateAsync(user);
                if (!identityResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, identityResult.Errors);
                }

                var roleName = (await _roleManager.FindByIdAsync(createEmployeeRequest.RoleId))?.Name;

                if (string.IsNullOrEmpty(roleName))
                {
                    transaction.Rollback();
                    return BadRequest("Role not found.");
                }

                var isInRole = await _userManager.IsInRoleAsync(user, roleName);
                if (isInRole)
                {
                    transaction.Commit();
                    return Ok();
                }
                else
                {
                    var prevRole = await _userManager.GetRolesAsync(user);
                    var removeroleResult = await _userManager.RemoveFromRolesAsync(user, prevRole);
                    var updateRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                    if (removeroleResult.Succeeded && updateRoleResult.Succeeded)
                    {
                        transaction.Commit();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Role cannot be updated.");
                    }
                }

            }
        }

        [HttpGet]
        [Route("DisableLogin/{userId}")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> DisableLogin(string userId)
        {
            var requestedBy = User.FindFirst("UserId").ToString();

            var user = await _userManager.FindByIdAsync(userId);
            var role = await _userManager.GetRolesAsync(user);

            if (user == null) return BadRequest("User not found.");
            if (role.FirstOrDefault() == SystemRoles.SuperAdmin) return BadRequest("User cannot be disabled.");

            user.IsDisabled = true;
            user.LastUpdatedBy = requestedBy;
            user.LastUpdatedDate = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded) return Ok();
            else return BadRequest("Error while disabling user.");

        }

        [HttpGet]
        [Route("EnableLogin/{userId}")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> EnableLogin(string userId)
        {
            var requestedBy = User.FindFirst("UserId").ToString();

            var user = await _userManager.FindByIdAsync(userId);
            var role = await _userManager.GetRolesAsync(user);

            if (user == null) return BadRequest("User not found.");
            if (role.FirstOrDefault() == SystemRoles.SuperAdmin) return BadRequest("User cannot be enabled/disabled.");

            user.IsDisabled = false;
            user.LastUpdatedBy = requestedBy;
            user.LastUpdatedDate = DateTime.UtcNow;

            var updateResult = await _userManager.UpdateAsync(user);

            if (updateResult.Succeeded) return Ok();
            else return BadRequest("Error while enabling user.");

        }
    }
}
