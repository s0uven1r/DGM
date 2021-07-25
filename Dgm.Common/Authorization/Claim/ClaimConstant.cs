using Dgm.Common.Authorization.Claim.Identity;
using Dgm.Common.Authorization.Claim.Resource;
using Dgm.Common.Authorization.MenuControl.Identity;
using Dgm.Common.Authorization.MenuControl.Resource;
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
                     Rank = 2,
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

                #region Vehicle
                 new SeedClaimViewModel { Id = "e41bc2af-2c61-4a1e-927f-72488df72800",
                     ClaimValue = VehicleClaimConstant.ViewVehicle,
                     MenuId = "a17d4044-8dad-4058-9488-841d3332d270",
                     Alias = VehicleMenuConstant.VehicleManagement.Value,
                     Title = VehicleMenuConstant.VehicleManagement.Key,
                     Class= "text-primary",
                     FaClass= "fa-car",
                     ParentId = null,
                     Rank = 2,
                     RouteUrl = "vehicle",
                     ClaimModule = PermissionModuleConstant.Vehicle,
                     ClaimTitle = "View Vehicle Management"
                 },
                 new SeedClaimViewModel { Id = "e41bc2af-2c61-4a1e-927f-72488df72801",
                     ClaimValue = VehicleClaimConstant.ViewRegistration,
                     MenuId = "a17d4044-8dad-4058-9488-841d3332d271",
                     Alias = VehicleMenuConstant.Registration.Value,
                     Title = VehicleMenuConstant.Registration.Key,
                     Class= "text-primary",
                     FaClass= "",
                     ParentId = "a17d4044-8dad-4058-9488-841d3332d270",
                     Rank = 1,
                     RouteUrl = "vehicle/register",
                     ClaimModule = PermissionModuleConstant.Vehicle,
                     ClaimTitle = "View Vehicle Registration"
                 },
                 new SeedClaimViewModel { Id = "e41bc2af-2c61-4a1e-927f-72488df72802",
                     ClaimValue = VehicleClaimConstant.ViewMaintenance,
                     MenuId = "a17d4044-8dad-4058-9488-841d3332d272",
                     Alias = VehicleMenuConstant.Maintenance.Value,
                     Title = VehicleMenuConstant.Maintenance.Key,
                     Class= "text-primary",
                     FaClass= "",
                     ParentId = "a17d4044-8dad-4058-9488-841d3332d270",
                     Rank = 2,
                     RouteUrl = "vehicle/maintain",
                     ClaimModule = PermissionModuleConstant.Vehicle,
                     ClaimTitle = "View Vehicle Maintenance"
                 },
                 new SeedClaimViewModel { Id = "e41bc2af-2c61-4a1e-927f-72488df72803",
                     ClaimValue = VehicleClaimConstant.WriteRegistration,
                     ClaimModule = PermissionModuleConstant.Vehicle,
                     ClaimTitle = "Write Vehicle Registration"
                 },
                 new SeedClaimViewModel { Id = "e41bc2af-2c61-4a1e-927f-72488df72804",
                     ClaimValue = VehicleClaimConstant.WriteMaintenance,
                     ClaimModule = PermissionModuleConstant.Vehicle,
                     ClaimTitle = "Write Vehicle Maintenance"
                 },
                #endregion

                #region Accounting
                new SeedClaimViewModel{
                    Id ="8e5b2b58-a2c1-4a9e-92ee-6037f476cb3d",
                    ClaimValue = AccountingClaimConstant.ViewAccounting,
                    MenuId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e41",
                    Alias = AccountMenuConstant.Accounting.Value,
                    Title = AccountMenuConstant.Accounting.Key,
                    Class= "text-primary",
                    FaClass= "money-check-edit-alt",
                    ParentId = null,
                    Rank = 3,
                    RouteUrl = "accounting",
                    ClaimModule = PermissionModuleConstant.Accounting,
                    ClaimTitle = "View Acconting"
                },
                new SeedClaimViewModel
                {
                     Id ="8e5b2b58-a2c1-4a9e-92ee-6037f476cb4e",
                     ClaimValue = AccountingClaimConstant.ViewAccountingType,
                     MenuId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e42",
                     Title = AccountMenuConstant.AccountType.Key,
                     Alias = AccountMenuConstant.AccountType.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e41",
                     Rank = 1,
                     RouteUrl = "accounting/accountingtype",
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "View Accounting Type"
                },
                new SeedClaimViewModel
                {
                     Id ="8e5b2b58-a2c1-4a9e-92ee-6037f476cb5f",
                     ClaimValue = AccountingClaimConstant.ViewAccountingHead,
                     MenuId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e43",
                     Title = AccountMenuConstant.AccountHead.Key,
                     Alias = AccountMenuConstant.AccountHead.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e41",
                     Rank = 2,
                     RouteUrl = "accounting/accountinghead",
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "View Accounting Head"
                },
                new SeedClaimViewModel { 
                     Id = "8e5b2b58-a2c1-4a9e-92ee-6037f476cb6g",
                     ClaimValue = AccountingClaimConstant.WriteAccountingType,
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "Write Accounting Type"
                 },
                new SeedClaimViewModel { Id = "8e5b2b58-a2c1-4a9e-92ee-6037f476cb7h",
                     ClaimValue = AccountingClaimConstant.WriteAccountingHead,
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "Write Accounting Head"
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
