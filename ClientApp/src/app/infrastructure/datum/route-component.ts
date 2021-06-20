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
      UserCreateRouteComponent: [CreateComponent]
      
}