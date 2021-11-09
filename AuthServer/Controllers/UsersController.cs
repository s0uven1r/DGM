using Auth.Infrastructure.Constants;
using AuthServer.Entities;
using AuthServer.Filters.AuthorizationFilter;
using AuthServer.Helpers;
using AuthServer.Models.EmailSender;
using AuthServer.Models.Users;
using AuthServer.Models.Users.Customer;
using AuthServer.Models.Users.Employee.Request;
using AuthServer.Persistence;
using AuthServer.Services.EmailSender;
using AuthServer.Services.Resource;
using AutoMapper;
using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Constants.KYC;
using Dgm.Common.Enums;
using Dgm.Common.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, AppIdentityDbContext appIdentityDbContext,
            IEmailSender emailSender, IAccountService accountService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appIdentityDbContext = appIdentityDbContext;
            _emailSender = emailSender;
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetUser")]
        [ApiAuthorize(IdentityClaimConstant.ViewUser)]
        public async Task<IActionResult> GetUser()
        {
            int rank = Convert.ToInt32(User.Claims.Where(x => x.Type == "RoleRank").FirstOrDefault().Value);
            var joinResult = await (from user in _appIdentityDbContext.Users
                                    join userRole in _appIdentityDbContext.UserRoles
                                    on user.Id equals userRole.UserId
                                    join role in _appIdentityDbContext.Roles
                                    on userRole.RoleId equals role.Id
                                    where role.Rank > rank
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
                IsEnabled = !x.user.IsDisabled,
                AccountNumber = x.user.AccountNumber
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
                IsDefault = role.IsDefault,
                AccountNumber = user.AccountNumber
            };
            return Ok(userResult);
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

            var transaction = await _appIdentityDbContext.Database.BeginTransactionAsync();
            try
            {
                var role = (await _roleManager.FindByIdAsync(createEmployeeRequest.RoleId));
                var roleName = role?.Name;

                if (string.IsNullOrEmpty(roleName))
                {
                    return BadRequest("Role not found.");
                }

                var password = PasswordGenerator.GenerateRandomPassword();

                var identityResult = await _userManager.CreateAsync(applicationUser, password);

                if (!identityResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, identityResult.Errors);
                }

                var roleResult = await _userManager.AddToRoleAsync(applicationUser, roleName);
               
                var accTypeName = Enum.GetName(typeof(RoleTypeEnum), role.Type);
                var alias = RoleTypeEnumConversion.GetDescriptionByValue(role.Type);
                if (string.IsNullOrEmpty(alias)) throw new Exception("Cannot get alias for Account Number");
                var accNo = await _accountService.GetAccountNumber(accTypeName, alias);
                applicationUser.AccountNumber = accNo;
                await _userManager.UpdateAsync(applicationUser);

                await _appIdentityDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                if (roleResult.Succeeded)
                {
                    await SendEmployeeRegistrationEmail(createEmployeeRequest, password);
                    return Ok();
                }
                else
                {
                    await _userManager.DeleteAsync(applicationUser);
                    return BadRequest("Error while adding role.");
                }
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
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
                try
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
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
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

        [HttpGet]
        [Route("GetAccountDetails")]
        public async Task<IActionResult> GetAccountDetails(string value)
        {
            var data = await (from user in _appIdentityDbContext.Users
                              join userRole in _appIdentityDbContext.UserRoles
                              on user.Id equals userRole.UserId
                              join role in _appIdentityDbContext.Roles
                              on userRole.RoleId equals role.Id
                              where role.Type == (int)RoleTypeEnum.Customer && !string.IsNullOrEmpty(user.AccountNumber)
                              select new
                              {
                                  user.Email,
                                  user.FirstName,
                                  user.MiddleName,
                                  user.LastName,
                                  user.AccountNumber
                              }).ToListAsync();
            if (data.Count == 0) return NotFound(); ;
            var accountDetails = data.Where(x => x.Email.Contains(value))
                .Select(x => new KeyValuePair<string, string>(string.Join(" ", x.FirstName, x.MiddleName, x.LastName, "-", x.AccountNumber), x.AccountNumber));

            return Ok(accountDetails.ToList());
        }
        [HttpGet]
        [Route("GetAccountTrainerDetails")]
        public async Task<IActionResult> GetAccountTrainerDetails(string value)
        {
            var data = await (from user in _appIdentityDbContext.Users
                              join userRole in _appIdentityDbContext.UserRoles
                              on user.Id equals userRole.UserId
                              join role in _appIdentityDbContext.Roles
                              on userRole.RoleId equals role.Id
                              where role.Type == (int)RoleTypeEnum.Employee && !string.IsNullOrEmpty(user.AccountNumber)
                              select new
                              {
                                  user.Email,
                                  user.FirstName,
                                  user.MiddleName,
                                  user.LastName,
                                  user.AccountNumber
                              }).ToListAsync();

            if (data.Count == 0) return NotFound(); ;
            var accountDetails = data.Where(x => x.Email.Contains(value))
                .Select(x => new KeyValuePair<string, string>(string.Join(" ", x.FirstName, x.MiddleName, x.LastName, "-", x.AccountNumber), x.AccountNumber));

            return Ok(accountDetails.ToList());
        }
        [HttpPost]
        [Route("UpdateKYC")]
        public async Task<IActionResult> UpdateKYC([FromBody] UserKYCRequestModel kycModel)
        {
            var requestedBy = User.FindFirst("UserId").Value.ToString();
            var user = await _userManager.Users.Where(u => u.Id == requestedBy).FirstOrDefaultAsync();
            if (user == null) return BadRequest("User not found");

            UserKYC kyc = _mapper.Map<UserKYC>(kycModel);
            kyc.UserId = requestedBy;
            kyc.CreatedBy = requestedBy;
            kyc.CreatedDate = DateTime.UtcNow;

            user.IsKYCUpdated = true;
            user.LastUpdatedBy = requestedBy;
            user.LastUpdatedDate = DateTime.UtcNow;

            var transaction = await _appIdentityDbContext.Database.BeginTransactionAsync();
            try
            {
                await _appIdentityDbContext.UserKYC.AddAsync(kyc);
                _appIdentityDbContext.Users.Update(user);
                await _appIdentityDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        [HttpGet]
        [Route("GetKYC/{userId}")]
        public async Task<IActionResult> GetKYC(string userId)
        {
            var kyc = await _appIdentityDbContext.UserKYC.OrderByDescending(x => x.CreatedDate).Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (kyc == null)
            {
                return BadRequest("Kyc not found!");
            }

            var kycResponse = _mapper.Map<UserKYCResponseModel>(kyc);
            return Ok(kycResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetKYCDDL")]
        public IActionResult GetKYCDDL()
        {
            var obj = new
            {
                MaritalStatusDDL = MaritalStatus.TypeOfMaritalStatus,
                BloodGroupDDL = BloodGroupConstant.TypeOfBloodGroup,
                GenderDDL = GenderConstant.TypeOfGender
            };
            return Ok(obj);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterCustomerPackage")]
        public async Task<IActionResult> RegisterCustomerPackage([FromBody] RegisterCustomerPackageViewModel model)
        {
            var userDetails = await _userManager.FindByEmailAsync(model.CustomerDetail.Email);
            var accNo = string.Empty;
            if (userDetails != null)
                accNo = userDetails.AccountNumber;
            var roleType = (int)RoleTypeEnum.Customer;
            var accTypeName = Enum.GetName(typeof(RoleTypeEnum), roleType);
            var alias = RoleTypeEnumConversion.GetDescriptionByValue(roleType);
            if (string.IsNullOrEmpty(alias)) throw new Exception("Cannot get alias for Account Number");
            try
            {
                accNo = await _accountService.RegisterCustomerPackage(accTypeName,
                 alias,
                 accNo,
                 model.StartDate,
                 model.StartDateNP,
                 model.EndDate,
                 model.EndDateNP,
                 model.PackageId,
                 model.ShiftId,
                 model.PaymentGateway,
                 model.PaidAmount,
                 model.PromoCode
                 );
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }

            if (userDetails == null)
            {
                var user = new AppUser
                {
                    UserName = model.CustomerDetail.UserName,
                    FirstName = model.CustomerDetail.FirstName,
                    MiddleName = model.CustomerDetail.MiddleName,
                    LastName = model.CustomerDetail.LastName,
                    Email = model.CustomerDetail.Email,
                    AccountNumber = accNo
                };
                if (model.IsAdmin)
                    model.CustomerDetail.Password = PasswordGenerator.GenerateRandomPassword();

                var userResult = await _userManager.CreateAsync(user, model.CustomerDetail.Password);

                if (!userResult.Succeeded) return BadRequest(userResult.Errors);

                var roleResult = await _userManager.AddToRoleAsync(user, SystemRoles.Consumer);
                if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);
            }
            return Ok();
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
