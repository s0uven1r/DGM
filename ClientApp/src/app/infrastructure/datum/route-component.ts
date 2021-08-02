import { AppComponent } from "src/app/app.component";
import { HomeComponent } from "src/app/core/home/home.component";
import { AuthCallbackComponent } from "src/app/core/auth-callback/auth-callback.component";
import { DashboardComponent } from "src/app/core/dashboard/dashboard.component";
import { RoleComponent } from "src/app/featured/identity/role/role.component";
import { PermissionComponent } from "src/app/featured/identity/permission/permission.component";
import { UserComponent } from "src/app/featured/identity/user/user.component";
import { CreateComponent } from "src/app/featured/identity/user/create/create.component";
import { ForbiddenComponent } from "src/app/core/forbidden/forbidden.component";
import { InternalServerErrorComponent } from "src/app/core/internal-server-error/internal-server-error.component";
import { MaintenanceComponent } from "src/app/featured/vehicle/maintenance/maintenance.component";
import { VehicleRegisterComponent } from "src/app/featured/vehicle/register/register.component";
import { VehicleCreateComponent } from "src/app/featured/vehicle/register/vehicle-create/vehicle-create.component";
import { VehicleUpdateComponent } from "src/app/featured/vehicle/register/vehicle-update/vehicle-update.component";
import { CreatemaintenanceComponent } from "src/app/featured/vehicle/maintenance/createmaintenance/createmaintenance.component";
import { AccountTypeComponent } from "src/app/featured/account/account-type/account-type.component";
import { AccountHeadComponent } from "src/app/featured/account/account-head/account-head.component";
import { AccountTypeCreateComponent } from "src/app/featured/account/account-type/account-type-create/account-type-create.component";
import { AccountTypeEditComponent } from "src/app/featured/account/account-type/account-type-edit/account-type-edit.component";
import { AccountHeadCreateComponent } from "src/app/featured/account/account-head/account-head-create/account-head-create.component";
import { AccountHeadEditComponent } from "src/app/featured/account/account-head/account-head-edit/account-head-edit.component";

export const RouteComponent = {
    AppRouteComponent: [
        AppComponent,
        HomeComponent,
        DashboardComponent,
        ForbiddenComponent,
        InternalServerErrorComponent
      ],
      AuthCallbackRouteComponent: [
        AuthCallbackComponent
      ],
      DashBoardRouteComponent: [],
      RoleRouteComponent: [RoleComponent],
      PermissionRouteComponent: [PermissionComponent],
      UserRouteComponent: [UserComponent],
      UserCreateRouteComponent: [CreateComponent],
      VehicleInventoryRouteComponent: [VehicleRegisterComponent],
      VehicleInventoryCreateRouteComponent: [VehicleCreateComponent],
      VehicleInventoryUpdateRouteComponent: [VehicleUpdateComponent],
      VehicleMaintenanceRouteComponent: [MaintenanceComponent],
      VehicleMaintenanceCreateRouteComponent: [CreatemaintenanceComponent],
      AccountTypeRouteComponent: [AccountTypeComponent],
      AccountTypeCreateRouteComponent: [AccountTypeCreateComponent],
      AccountTypeEditRouteComponent: [AccountTypeEditComponent],
      AccountHeadRouteComponent: [AccountHeadComponent],
      AccountHeadCreateRouteComponent: [AccountHeadCreateComponent],
      AccountHeadEditRouteComponent: [AccountHeadEditComponent],
}