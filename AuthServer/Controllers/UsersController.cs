using Auth.Infrastructure.Constants;
using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Helpers;
using AuthServer.Models.EmailSender;
using AuthServer.Models.Users;
using AuthServer.Models.Users.Employee.Request;
using AuthServer.Services.EmailSender;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
        private readonly IEmailSender _emailSender;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppIdentityDbContext appIdentityDbContext,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appIdentityDbContext = appIdentityDbContext;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        [ApiAuthorize(IdentityClaimConstant.ViewUser)]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.Select(x => new GetUserResponse
            {
                Id = x.Id,
                Email = x.Email,
                UserName = x.UserName,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber
            }).ToList();
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPost]
        [Route("CreateEmployee")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();

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

            var password = PasswordGenerator.GenerateRandomPassword();
            var identityResult = await _userManager.CreateAsync(applicationUser, password);

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
                await SendEmployeeRegistrationEmail(createEmployeeRequest, password);
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
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest updateEmployeeRequest)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            using (var transaction = _appIdentityDbContext.Database.BeginTransaction())
            {

                var user = await _userManager.FindByIdAsync(updateEmployeeRequest.Id);
                if (user == null)
                {
                    return BadRequest("User not found.");
                }

                user.FirstName = updateEmployeeRequest.FirstName;
                user.MiddleName = updateEmployeeRequest.MiddleName;
                user.LastName = updateEmployeeRequest.LastName;
                user.PhoneNumber = updateEmployeeRequest.Phone;
                user.LastUpdatedBy = requestedBy;
                user.LastUpdatedDate = DateTime.UtcNow;


                var identityResult = await _userManager.UpdateAsync(user);
                if (!identityResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, identityResult.Errors);
                }

                var roleName = (await _roleManager.FindByIdAsync(updateEmployeeRequest.RoleId))?.Name;

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


        [HttpPost]
        [Route("UpdateEmployeePassword")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> UpdateEmployeePassword([FromBody] UpdateEmployeePasswordRequest updateEmployeeRequest)
        {
            if (updateEmployeeRequest.NewPassword != updateEmployeeRequest.NewConfirmPassword)
                return BadRequest("New passwords didnot matched.");

            var user = await _userManager.FindByIdAsync(updateEmployeeRequest.Id);
            if (user == null)
                return BadRequest("User not found");

            var passwordChangeResult = await _userManager.ChangePasswordAsync(user, updateEmployeeRequest.Password, updateEmployeeRequest.NewPassword);

            if (passwordChangeResult.Succeeded) return Ok();
            
            return BadRequest("Error while changing password.");
        }

        [HttpGet]
        [Route("DisableLogin/{userId}")]
        [ApiAuthorize(IdentityClaimConstant.WriteUser)]
        public async Task<IActionResult> DisableLogin(string userId)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();

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
            var requestedBy = User.FindFirst("UserId").Value.ToString();

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

        #region helpers
        private async Task SendEmployeeRegistrationEmail(CreateEmployeeRequest model, string password)
        {
            EmailSenderRequest emailSenderRequest = new EmailSenderRequest
            {
                ToEmail = model.Email,
                Subject = "Sucessfully registered a account."
            };

            string FilePath = Directory.GetCurrentDirectory() + "\\Views\\EmailTemplates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", model.UserName).Replace("[email]", model.Email).Replace("[password]", password);

            emailSenderRequest.Body = MailText;

            await _emailSender.SendEmailAsync(emailSenderRequest);

            // _logger.LogInformation(3, "Employee User created a new account with password.");
        }
        #endregion

    }
}
