import { AppComponent } from "src/app/app.component";
import { HomeComponent } from "src/app/core/home/home.component";
import { CounterComponent } from "src/app/featured/counter/counter.component";
import { AuthCallbackComponent } from "src/app/featured/auth-callback/auth-callback.component";
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

export const RouteComponent = {
    AppRouteComponent: [
        AppComponent,
        HomeComponent,
        DashboardComponent,
        ForbiddenComponent,
        InternalServerErrorComponent
      ],
      CounterRouteComponent: [
        CounterComponent
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
      
}