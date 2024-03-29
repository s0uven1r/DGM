﻿using Auth.Infrastructure.Constants;
using Auth.Infrastructure.Identity;
using Auth.Infrastructure.Persistence;
using AuthServer.Filters.AuthorizationFilter;
using Dgm.Common.Authorization.Claim.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace AuthServer.Controllers
{
    [Route("Authorization/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    public class MenuController : Controller
    {
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public MenuController(AppIdentityDbContext appIdentityDbContext)
        {
            _appIdentityDbContext = appIdentityDbContext;
        }

        [HttpGet]
        [Route("GetMenu")]
        [ApiAuthorize(IdentityClaimConstant.ViewRole)]
        public async Task<IActionResult> GetMenu()
        {
            IQueryable<MenuControl> menuControl;
            if (User.IsInRole(SystemRoles.SuperAdmin))
            {
                menuControl = _appIdentityDbContext.MenuControl.Include(x => x.Children)
                        .Where(x => x.ParentId == null).AsQueryable();
            }
            else
            {
                var permissionList = User.Claims.Where(x => x.Type == "permissionIds").Select(a => a.Value).ToList();
                menuControl = _appIdentityDbContext.MenuControl.Include(x => x.Children.Where(i => permissionList.Any(a => a == i.ClaimId)))
                       .Where(x => x.ParentId == null && permissionList.Any(a => a == x.ClaimId)).AsQueryable();
            }
            var menu = await menuControl.ToListAsync();
            return Ok(menu);
        }
    }
}
