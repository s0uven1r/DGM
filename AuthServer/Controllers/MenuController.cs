using Auth.Infrastructure.Persistence;
using AuthServer.Filters.AuthorizationFilter;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class MenuController : Controller
    {
       
        private readonly ILogger<AccountController> _logger;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public MenuController( ILogger<AccountController> logger, AppIdentityDbContext appIdentityDbContext)
        {
            _logger = logger;
            _appIdentityDbContext = appIdentityDbContext;
        }

        [HttpGet]
        [Route("GetMenu")]
        [ApiAuthorize(IdentityClaimConstant.ViewRole)]
        public IActionResult GetMenu()
        {
            var permissionList = User.Claims
               .Where(x => x.Type == "permissionIds")
               .Select(a => a.Value).ToList();

            var data = _appIdentityDbContext.MenuControl.Include(x => x.Children.Where(i => permissionList.Any(a => a == i.ClaimId)))
                .Where(x => x.ParentId == null && permissionList.Any(a => a == x.ClaimId)).ToList();
            if (data == null) return NotFound();
            return Ok(data);
        }
    }
}
