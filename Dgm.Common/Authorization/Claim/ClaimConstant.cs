using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Authorization.MenuControl.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dgm.Common.Authorization.Claim
{
    public static class ClaimConstant
    {
        public static List<SeedClaimViewModel> ClaimList()
        {
            return new List<SeedClaimViewModel>()
            {
                #region identity
                 new SeedClaimViewModel { Id = "7f51ab29-844a-470e-9172-42cde237dad9",
                     ClaimValue=IdentityClaimConstant.ViewIdentity,
                     MenuId = "7f51ab29-844a-470e-9172-42cde237dad0",
                     Alias = IdentityMenuConstant.Identity.Value,
                     Title = IdentityMenuConstant.Identity.Key,
                     Class= "text-primary",
                     FaClass= "fa-user",
                     ParentId = null,
                     Rank = 1,
                     RouteUrl = "/identity"
                 },
                  new SeedClaimViewModel { Id = "7f51ab29-844a-470e-9172-42cde237dae7",
                     ClaimValue=IdentityClaimConstant.CreateIdentity,
                     MenuId = "7f51ab29-844a-470e-9172-42cde237dae0",
                     Alias = IdentityMenuConstant.IdentityCreate.Value,
                     Title = IdentityMenuConstant.IdentityCreate.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = "7f51ab29-844a-470e-9172-42cde237dad0",
                     Rank = 1,
                     RouteUrl = "/registration"
                 },
                #endregion
            };
        }
    }
}
