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
                     RouteUrl = "/identity",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View Identity"
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
                     RouteUrl = "/registration",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Create Identity"
                 },
                 new SeedClaimViewModel { Id = "3a8e8e1d-ab8b-40af-9b94-4523885966f0",
                     ClaimValue=IdentityClaimConstant.ViewRole,
                     MenuId = "107df68e-f076-4edc-8c05-8188b955e960",
                     Alias = IdentityMenuConstant.Role.Value,
                     Title = IdentityMenuConstant.Role.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = null,
                     Rank = 1,
                     RouteUrl = "/role",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View Role"
                 },
                 new SeedClaimViewModel { Id = "5465f04d-ea46-400e-bf68-84d9108a9344",
                     ClaimValue=IdentityClaimConstant.CreateRole,
                     MenuId = "88691d8e-9788-4105-861a-db3e942843b4",
                     Alias = IdentityMenuConstant.RoleCreate.Value,
                     Title = IdentityMenuConstant.RoleCreate.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = "107df68e-f076-4edc-8c05-8188b955e960",
                     Rank = 1,
                     RouteUrl = "/role/create",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Create Role"
                 },
                 new SeedClaimViewModel { Id = "05c127b6-a88a-44fc-a319-4d3ef7354ac7",
                     ClaimValue=IdentityClaimConstant.UpdateRole,
                     MenuId = "9f7b0fe0-4c5f-4689-938e-3952937a359c",
                     Alias = IdentityMenuConstant.RoleUpdate.Value,
                     Title = IdentityMenuConstant.RoleUpdate.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = "107df68e-f076-4edc-8c05-8188b955e960",
                     Rank = 2,
                     RouteUrl = "/role/update",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Update Role"
                 },
                 new SeedClaimViewModel { Id = "68cc24d2-36c3-4b80-97a7-14ece753a318",
                     ClaimValue=IdentityClaimConstant.ViewPermission,
                     MenuId = "0d5faddd-ddce-4a27-ae0e-d55a791c9f8f",
                     Alias = IdentityMenuConstant.Permission.Value,
                     Title = IdentityMenuConstant.Permission.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = null,
                     Rank = 1,
                     RouteUrl = "/permission",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "View Permission"
                 },
                 new SeedClaimViewModel { Id = "0c626829-5d91-46f1-b3a8-c3fa6541d633",
                     ClaimValue=IdentityClaimConstant.UpdatePermission,
                     MenuId = "ed0fdcb8-a987-4730-bf85-2c4956bd00bd",
                     Alias = IdentityMenuConstant.PermissionUpdate.Value,
                     Title = IdentityMenuConstant.PermissionUpdate.Key,
                     Class= "text-primary",
                     FaClass= "fa-plus",
                     ParentId = "0d5faddd-ddce-4a27-ae0e-d55a791c9f8f",
                     Rank = 1,
                     RouteUrl = "/permission/update",
                     ClaimModule = PermissionModuleConstant.Authorization,
                     ClaimTitle = "Update Permission"
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
