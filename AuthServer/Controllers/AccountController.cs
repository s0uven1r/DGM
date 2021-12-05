using AuthServer.Entities;
using AuthServer.Extensions;
using AuthServer.Models;
using AuthServer.Models.EmailSender;
using AuthServer.Persistence;
using AuthServer.Services.EmailSender;
using AuthServer.Services.Resource;
using Dgm.Common.Enums;
using Dgm.Common.Models;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly IEmailSender _emailSender;
        private readonly IAccountService _accountService;
        private readonly AppIdentityDbContext _appIdentityDbContext;
        private readonly IOptions<ClientBaseUrls> _clientUrls;


        public AccountController(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager,
            IIdentityServerInteractionService interaction,
            IAuthenticationSchemeProvider schemeProvider,
            IClientStore clientStore, IEventService events,
            ILogger<AccountController> logger,
            IEmailSender emailSender,
            IAccountService accountService,
            AppIdentityDbContext appIdentityDbContext,
           IOptions<ClientBaseUrls> clientUrls
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _interaction = interaction;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
            _events = events;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _accountService = accountService;
            _appIdentityDbContext = appIdentityDbContext;
            _clientUrls = clientUrls;
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
            ViewBag.LogoImage = GetLogoImage();
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
            ViewBag.LogoImage = GetLogoImage();
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
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && !user.IsDisabled)
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

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await BuildRegisterViewModelAsync(returnUrl);
            ViewBag.LogoImage = GetLogoImage();
            var publicRole = _roleManager.Roles.Where(x => x.IsPublic == true);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ViewBag.LogoImage = GetLogoImage();

            var role = _roleManager.Roles.Where(x => x.IsPublic == true).FirstOrDefault();
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Public user cannot be registered in this system.");
                var vm = await BuildRegisterViewModelAsync(model.ReturnUrl);
                return View(vm);
            }
            if (await _userManager.FindByEmailAsync(model.Username) != null)
            {
                ModelState.AddModelError(string.Empty, "Use already registered with same username.");
                var vm = await BuildRegisterViewModelAsync(model.ReturnUrl);
                return View(vm);
            }
            if (await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError(string.Empty, "Use already registered with same email.");
                var vm = await BuildRegisterViewModelAsync(model.ReturnUrl);
                return View(vm);
            }

            var user = new AppUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                LockoutEnabled = true,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Email.ToUpper()
            };

            var userResult = await _userManager.CreateAsync(user, model.Password);

            if (!userResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, userResult.Errors.FirstOrDefault().Description);
                var vm = await BuildRegisterViewModelAsync(model.ReturnUrl);
                return View(vm);
            }

            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, userResult.Errors.FirstOrDefault().Description);
                var vm = await BuildRegisterViewModelAsync(model.ReturnUrl);
                return View(vm);
            }

            var accTypeName = Enum.GetName(typeof(RoleTypeEnum), role.Type);
            var alias = RoleTypeEnumConversion.GetDescriptionByValue(role.Type);
            if (string.IsNullOrEmpty(alias)) throw new Exception("Cannot get alias for Account Number");
            var accNo = await _accountService.GetAccountNumber(accTypeName, alias);
            var userCreated = await _userManager.FindByEmailAsync(model.Email);
            userCreated.AccountNumber = accNo;
            await _userManager.UpdateAsync(userCreated);

            return Redirect(model.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            await _signInManager.SignOutAsync();
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            return Redirect(context.PostLogoutRedirectUri ?? $"{_clientUrls.Value.AuthServer}auth-callback");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPassword)
        {
            if (!ModelState.IsValid) return View(forgotPassword);

            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callBack = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            await SendEmailForForgotPasswordToken(callBack, user.Email);

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordViewModel
            {
                Token = token,
                Email = email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return RedirectToAction(nameof(ResetPasswordConfirmation));

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!resetPasswordResult.Succeeded)
            {

                foreach (var error in resetPasswordResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }


        [HttpGet]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
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

        private async Task<RegisterViewModel> BuildRegisterViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new RegisterViewModel
                {
                    EnableLocalLogin = local,
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

            return new RegisterViewModel
            {
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task SendEmailForForgotPasswordToken(string message, string email)
        {
            var mailRequest = new EmailSenderRequest
            {
                ToEmail = email,
                Subject = "Reset Password link.",
                Body = message
            };
            await _emailSender.SendEmailAsync(mailRequest);
        }

        private string GetLogoImage() => $"defaultLogo.jpg";
    }
}
