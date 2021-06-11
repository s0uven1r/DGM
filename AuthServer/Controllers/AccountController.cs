﻿using Microsoft.AspNetCore.Mvc;
using AuthServer.Models;
using System.Threading.Tasks;
using IdentityServer4.Services;
using System.Linq;
using System;
using IdentityServer4.Stores;
using IdentityServer4.Models;
using IdentityServer4.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Auth.Infrastructure.Identity;
using AuthServer.Extensions;
using IdentityServer4;
using Auth.Infrastructure.Constants;
using AuthServer.Models.Users.Employee.Response;
using AuthServer.Models.Users.Employee.Request;
using Microsoft.Extensions.Logging;

namespace AuthServer.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;
        private readonly ILogger<AccountController> _logger;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IIdentityServerInteractionService interaction, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore, IEventService events, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
            _events = events;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            AuthorizationRequest context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (await _clientStore.IsPkceClientAsync(context.Client.ClientId))
                    {
                        // if the client is PKCE then we assume it's native, so this change in how to
                        // return the response is for better UX for the end user.
                        return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                    }

                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                // validate username/password
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.FirstName));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    };

                    var isuser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.UserName
                    };

                    // issue authentication cookie with subject ID and username
                    await HttpContext.SignInAsync(isuser, props);

                    if (context != null)
                    {
                        if (await _clientStore.IsPkceClientAsync(context.Client.ClientId))
                        {
                            // if the client is PKCE then we assume it's native, so this change in how to
                            // return the response is for better UX for the end user.
                            //return View("Redirect", new RedirectViewModel { RedirectUrl = model.ReturnUrl });
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        _logger.LogError("Inavlid Return Url. User:{1} ReturnUrl: {2} TimeStamp:{3}", model.Username, model.ReturnUrl, DateTime.UtcNow);
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("Invalid return URL");
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequestViewModel model)
        {
            //var aVal = 0; var blowUp = 1 / aVal;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser { UserName = model.Email, FirstName = model.FirstName, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("username", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("firstname", user.FirstName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("middlename", user.MiddleName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("lastname", user.LastName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("role", SystemRoles.Consumer));

            return Ok(new RegisterResponseViewModel(user));
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            return Redirect(context.PostLogoutRedirectUri ?? "http://localhost:4200/auth-callback");
        }


        [HttpGet]
        public IActionResult GetEmployees()
        {
            var users = _userManager.Users.Select(x => new CreateEmployeeResponse
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
            });
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest createEmployeeRequest)
        {
            bool usernameExists = await _userManager.FindByNameAsync(createEmployeeRequest.UserName) != null;
            if (usernameExists)
            {
                return BadRequest("Username is taken.");
            }
            bool idExists = await _userManager.FindByIdAsync(createEmployeeRequest.Id) != null;
            if (idExists)
            {
                return BadRequest("ID must be unique.");
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
                LastName = createEmployeeRequest.LastName
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
                return NoContent();
            }
            else
            {
                await _userManager.DeleteAsync(applicationUser);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes.Where(x => x.DisplayName != null || (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }
    }
}
