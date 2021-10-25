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
        private static readonly List<SeedClaimViewModel> _list;
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
                    RouteUrl = "account",
                    ClaimModule = PermissionModuleConstant.Accounting,
                    ClaimTitle = "View Accounting"
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
                     RouteUrl = "account/accounttype",
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
                     RouteUrl = "account/accounthead",
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
                new SeedClaimViewModel
                {
                     Id ="7e5b2b57-a2c1-4a7e-92ee-7077f477cb4e",
                     ClaimValue = AccountingClaimConstant.ViewAccountingTransactionEntry,
                     MenuId = "0ff1f374-3c4f-4cf7-74eb-a7d072e72e47",
                     Title = AccountMenuConstant.TransactionEntry.Key,
                     Alias = AccountMenuConstant.TransactionEntry.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e41",
                     Rank = 3,
                     RouteUrl = "account/transactionentry",
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "View Accounting Transaction Entry"
                },
                new SeedClaimViewModel
                {    Id = "7e5b2b57-a2c1-7a9e-92ee-7037f477cb7h",
                     ClaimValue = AccountingClaimConstant.WriteAccountingTransactionEntry,
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "Write Accounting Transaction Entry"
                },
                new SeedClaimViewModel
                {
                     Id ="8e5b2b58-a2c1-4a8e-92ee-6037f109cb0c",
                     ClaimValue = AccountingClaimConstant.ViewCustomerPayment,
                     MenuId = "0ff1f370-3c4f-4cf7-74eb-a7d072e72e49",
                     Title = AccountMenuConstant.CustomerPaymentEntry.Key,
                     Alias = AccountMenuConstant.CustomerPaymentEntry.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "0ff1f384-3c4f-4cf9-84eb-a6d082e22e41",
                     Rank = 4,
                     RouteUrl = "account/customerpayment",
                     ClaimModule = PermissionModuleConstant.Accounting,
                     ClaimTitle = "View Customer Payment"
                },
                #endregion

                #region package and course
                 new SeedClaimViewModel{
                    Id ="08888830-37e2-4fb2-88c2-85d142868122",
                    ClaimValue = PackageCourseClaimConstant.ViewConfiguration,
                    MenuId = "b814543a-4048-40e9-aa95-6a711c5fffaa",
                    Alias = PackageCourseMenuConstant.ConfigurationManagement.Value,
                    Title = PackageCourseMenuConstant.ConfigurationManagement.Key,
                    Class= "text-primary",
                    FaClass= "fa-cogs",
                    ParentId = null,
                    Rank = 5,
                    RouteUrl = "config",
                    ClaimModule = PermissionModuleConstant.Configuration,
                    ClaimTitle = "View Configuration"
                },
                new SeedClaimViewModel
                {
                     Id ="08888831-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.ViewCourseType,
                     MenuId = "b814543b-4048-40e9-aa95-6a711c5fffbb",
                     Title = PackageCourseMenuConstant.CourseType.Key,
                     Alias =  PackageCourseMenuConstant.CourseType.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "b814543a-4048-40e9-aa95-6a711c5fffaa",
                     Rank = 1,
                     RouteUrl = "config/coursetype",
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "View Course Type"
                },

                new SeedClaimViewModel {
                     Id = "08888832-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.WriteCourse,
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "Write Course Type"
                 },
                 new SeedClaimViewModel
                {
                     Id ="08888833-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.ViewCourse,
                     MenuId = "b814543b-4048-40e9-aa95-6a711c5fffcc",
                     Title = PackageCourseMenuConstant.Course.Key,
                     Alias =  PackageCourseMenuConstant.Course.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "b814543a-4048-40e9-aa95-6a711c5fffaa",
                     Rank = 2,
                     RouteUrl = "config/course",
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "View Course"
                },
                new SeedClaimViewModel {
                     Id = "08888834-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.WriteCourse,
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "Write Course"
                 },
                 new SeedClaimViewModel
                {
                     Id ="08888835-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.ViewPackage,
                     MenuId = "b814543b-4048-40e9-aa95-6a711c5fffdd",
                     Title = PackageCourseMenuConstant.Package.Key,
                     Alias =  PackageCourseMenuConstant.Package.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "b814543a-4048-40e9-aa95-6a711c5fffaa",
                     Rank = 3,
                     RouteUrl = "config/package",
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "View package"
                },
                  new SeedClaimViewModel {
                     Id = "08888836-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.WritePackage,
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "Write Package"
                 },
                  new SeedClaimViewModel
                {
                     Id ="08888837-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.ViewPromo,
                     MenuId = "b814543b-4048-40e9-aa95-6a711c5fffee",
                     Title = PackageCourseMenuConstant.Promo.Key,
                     Alias =  PackageCourseMenuConstant.Promo.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "b814543a-4048-40e9-aa95-6a711c5fffaa",
                     Rank = 4,
                     RouteUrl = "config/promo",
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "View Promo"
                },
                  new SeedClaimViewModel {
                     Id = "08888838-37e2-4fb2-88c2-85d142868122",
                     ClaimValue = PackageCourseClaimConstant.Writepromo,
                     ClaimModule = PermissionModuleConstant.Configuration,
                     ClaimTitle = "Write promo"
                 },
                #endregion

                #region Settings
                new SeedClaimViewModel { Id = "7f81ab88-844a-880e-8178-48cde287aaa8",
                     ClaimValue=IdentityClaimConstant.ViewSettings,
                     MenuId = "8f51ac89-844a-470e-9172-42cde237aad8",
                     Alias = IdentityMenuConstant.Settings.Value,
                     Title = IdentityMenuConstant.Settings.Key,
                     Class= "text-primary",
                     FaClass= "tools",
                     ParentId = null,
                     Rank = 6,
                     RouteUrl = "settings",
                     ClaimModule = PermissionModuleConstant.Settings,
                     ClaimTitle = "View Settings"
                 },
                new SeedClaimViewModel
                {
                     Id ="8e5b2b58-a2c1-5a2e-29ee-3067f218cb1e",
                     ClaimValue = IdentityClaimConstant.ViewLogo,
                     MenuId = "0ac6f041-7a4c-7cf2-84eb-a2d011e14e24",
                     Title = IdentityMenuConstant.Logo.Key,
                     Alias = IdentityMenuConstant.Logo.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "8f51ac89-844a-470e-9172-42cde237aad8",
                     Rank = 1,
                     RouteUrl = "settings/logo",
                     ClaimModule = PermissionModuleConstant.Settings,
                     ClaimTitle = "View Logo"
                }
                ,
                new SeedClaimViewModel
                {
                     Id ="8e5b2b58-a2c1-5a2e-29ee-2135g238da4c",
                     ClaimValue = IdentityClaimConstant.ViewDescriptiveImages,
                     MenuId = "0ac6f041-7a4c-7cf2-84eb-b3c332c32d42",
                     Title = IdentityMenuConstant.DescriptiveImage.Key,
                     Alias = IdentityMenuConstant.DescriptiveImage.Value,
                     Class = "text-primary",
                     FaClass= "",
                     ParentId = "8f51ac89-844a-470e-9172-42cde237aad8",
                     Rank = 2,
                     RouteUrl = "settings/descriptiveimage",
                     ClaimModule = PermissionModuleConstant.Settings,
                     ClaimTitle = "View Descriptive Images"
                },
                #endregion

                #region shift
                new SeedClaimViewModel { Id = "9e587e5c-e35b-4a83-862a-6c62906a8443",
                     ClaimValue = ShiftClaimConstant.ViewShiftManagement,
                     MenuId = "a8fb9d05-3b2b-4fc6-a66c-396b56cee3e0",
                     Alias = ShiftMenuConstant.ShiftManagement.Value,
                     Title = ShiftMenuConstant.ShiftManagement.Key,
                     Class= "text-primary",
                     FaClass= "fa-clock-o",
                     ParentId = null,
                     Rank = 2,
                     RouteUrl = "shift-config",
                     ClaimModule = PermissionModuleConstant.Shift,
                     ClaimTitle = "View Shift Management"
                 },
                 new SeedClaimViewModel { Id = "9e587e5c-e35b-4a83-862a-6c62906a8444",
                     ClaimValue = ShiftClaimConstant.ViewShift,
                     MenuId = "a8fb9d05-3b2b-4fc6-a66c-396b56cee3e1",
                     Alias = ShiftMenuConstant.Shift.Value,
                     Title = ShiftMenuConstant.Shift.Key,
                     Class= "text-primary",
                     FaClass= "",
                     ParentId = "a8fb9d05-3b2b-4fc6-a66c-396b56cee3e0",
                     Rank = 1,
                     RouteUrl = "shift-config/shift",
                     ClaimModule = PermissionModuleConstant.Shift,
                     ClaimTitle = "View Shift"
                 },
                 new SeedClaimViewModel { Id = "9e587e5c-e35b-4a83-862a-6c62906a8445",
                     ClaimValue = ShiftClaimConstant.ViewShiftFrequency,
                     MenuId = "a8fb9d05-3b2b-4fc6-a66c-396b56cee3e2",
                     Alias = ShiftMenuConstant.ShiftFrequency.Value,
                     Title = ShiftMenuConstant.ShiftFrequency.Key,
                     Class= "text-primary",
                     FaClass= "",
                     ParentId = "a8fb9d05-3b2b-4fc6-a66c-396b56cee3e0",
                     Rank = 2,
                     RouteUrl = "shift-config/frequency",
                     ClaimModule = PermissionModuleConstant.Shift,
                     ClaimTitle = "View Shift Frequency"
                 },
                 new SeedClaimViewModel { Id = "9e587e5c-e35b-4a83-862a-6c62906a8446",
                     ClaimValue = ShiftClaimConstant.WriteShiftFrequency,
                     ClaimModule = PermissionModuleConstant.Shift,
                     ClaimTitle = "Write Shift Frequency"
                 },
                 new SeedClaimViewModel { Id = "9e587e5c-e35b-4a83-862a-6c62906a8447",
                     ClaimValue = ShiftClaimConstant.WriteShift,
                     ClaimModule = PermissionModuleConstant.Shift,
                     ClaimTitle = "Write Shift"
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
