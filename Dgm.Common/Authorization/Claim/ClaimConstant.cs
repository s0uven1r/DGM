using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Authorization.MenuControl.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Dgm.Common.Authorization.Claim
{
    public static class ClaimConstant
    {
        private static List<SeedClaimViewModel> _list;
        static ClaimConstant()
        {
            _list = new List<SeedClaimViewModel>()
            {
                #region User management
                 new SeedClaimViewModel { Id = "7f51ab29-844a-470e-9172-42cde237dad9",
                     ClaimValue=IdentityClaimConstant.ViewIdentity,
                     MenuId = "7f51ab29-844a-470e-9172-42cde237dad0",
                     Alias = IdentityMenuConstant.UserManagement.Value,
                     Title = IdentityMenuConstant.UserManagement.Key,
                     Class= "text-primary",
                     FaClass= "fa-user",
                     ParentId = null,
                     Rank = 1,
                     RouteUrl = "account",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View User Management"
                 },
                 new SeedClaimViewModel { Id = "7f51ab29-844a-470e-9172-42cde237dae7",
                     ClaimValue=IdentityClaimConstant.ViewUser,
                     MenuId = "7f51ab29-844a-470e-9172-42cde237dae0",
                     Alias = IdentityMenuConstant.User.Value,
                     Title = IdentityMenuConstant.User.Key,
                     Class= "text-primary",
                     FaClass= "",
                     ParentId = "7f51ab29-844a-470e-9172-42cde237dad0",
                     Rank = 1,
                     RouteUrl = "user",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View User"
                 },
                  new SeedClaimViewModel { Id = "05c127b6-a88a-44fc-a319-4d3ef7354ac7",
                     ClaimValue=IdentityClaimConstant.WriteUser,
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Write User"
                 },
                 new SeedClaimViewModel { Id = "3a8e8e1d-ab8b-40af-9b94-4523885966f0",
                     ClaimValue=IdentityClaimConstant.ViewRole,
                     MenuId = "107df68e-f076-4edc-8c05-8188b955e960",
                     Alias = IdentityMenuConstant.Role.Value,
                     Title = IdentityMenuConstant.Role.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = "7f51ab29-844a-470e-9172-42cde237dad0",
                     Rank = 1,
                     RouteUrl = "role",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View role"
                 },
                 new SeedClaimViewModel { Id = "5465f04d-ea46-400e-bf68-84d9108a9344",
                     ClaimValue=IdentityClaimConstant.WriteRole,
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Write Role"
                 },
                
                 new SeedClaimViewModel { Id = "68cc24d2-36c3-4b80-97a7-14ece753a318",
                     ClaimValue=IdentityClaimConstant.WritePermission,
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Write Permission"
                 },
                 new SeedClaimViewModel { Id = "0c626829-5d91-46f1-b3a8-c3fa6541d633",
                     ClaimValue=IdentityClaimConstant.ViewPermission,
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View Permission"
                 },
                #endregion
            };
        }
        public static List<SeedClaimViewModel> ClaimList()
        {
            return _list;
        }
        public static IQueryable<IGrouping<string, SeedClaimViewModel>> GetGroupedResult()
        {
            return _list.GroupBy(x => x.ClaimModule).AsQueryable();
        }
    }
}
