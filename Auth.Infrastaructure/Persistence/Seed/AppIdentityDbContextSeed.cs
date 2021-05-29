using Auth.Infrastructure.Identity;
using Dgm.Common.Authorization.Claim;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence.Seed
{
    public static class AppIdentityDbContextSeed
    {
        public async static Task SeedDefaultConfiguration(AppIdentityDbContext context)
        {

            context.Database.EnsureCreated();
            
            #region claim
            var claimCostData = ClaimConstant.ClaimList().Select(x => new ControllerClaim
            {
                Id = x.Id,
                ClaimType = ClaimType.Permission,
                ClaimValue = x.ClaimValue,
                ClaimModule = x.ClaimModule,
                ClaimTitle = x.ClaimTitle,
                IsActive = true
            }).ToList();

            claimCostData.RemoveAll(x => context.ControllerClaim.Select(y => y.Id).Contains(x.Id));
            if (claimCostData.Count > 0)
            {
                await context.ControllerClaim.AddRangeAsync(claimCostData);
            }
            #endregion claim 

            #region menu
            var menuCostData = ClaimConstant.ClaimList().Where(x => x.MenuId != null)
            .Select(x => new MenuControl
            {
                Id = x.MenuId,
                ClaimId = x.Id,
                Alias = x.Alias,
                Class = x.Class,
                FaClass = x.FaClass,
                ParentId = x.ParentId,
                Rank = x.Rank,
                RouteUrl = x.RouteUrl,
                Title = x.Title,
                IsActive = true
            }).ToList();
            menuCostData.RemoveAll(x => context.MenuControl.Select(y => y.Id).Contains(x.Id));
            if (menuCostData.Count > 0)
            {
                await context.MenuControl.AddRangeAsync(menuCostData);
            }
            #endregion

            await context.SaveChangesAsync();

        }
    }
}
