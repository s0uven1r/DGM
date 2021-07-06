import { AuthGuard } from 'src/app/core/authorize/auth-guard';
import { DashboardComponent } from 'src/app/core/dashboard/dashboard.component';
import { ForbiddenComponent } from 'src/app/core/forbidden/forbidden.component';
import { HomeComponent } from 'src/app/core/home/home.component';
import { InternalServerErrorComponent } from 'src/app/core/internal-server-error/internal-server-error.component';
import { UndefinedPageComponent } from 'src/app/core/undefined-page/undefined-page.component';
import { AuthCallbackComponent } from 'src/app/featured/auth-callback/auth-callback.component';
import { CounterComponent } from 'src/app/featured/counter/counter.component';
import { PermissionComponent } from 'src/app/featured/identity/permission/permission.component';
import { RoleComponent } from 'src/app/featured/identity/role/role.component';
import { RoleResolverService } from 'src/app/featured/identity/role/service/resolver/role-resolver.service';
import { CreateComponent } from 'src/app/featured/identity/user/create/create.component';
import { UserComponent } from 'src/app/featured/identity/user/user.component';
import { CreatemaintenanceComponent } from 'src/app/featured/vehicle/maintenance/createmaintenance/createmaintenance.component';
import { MaintenanceComponent } from 'src/app/featured/vehicle/maintenance/maintenance.component';
import { VehicleRegisterComponent } from 'src/app/featured/vehicle/register/register.component';
import { VehicleCreateComponent } from 'src/app/featured/vehicle/register/vehicle-create/vehicle-create.component';
import { VehicleUpdateComponent } from 'src/app/featured/vehicle/register/vehicle-update/vehicle-update.component';
import { VehicleResolverService } from 'src/app/featured/vehicle/service/vehicle-resolver.service';
import { PermissionRoutePath } from './route-path/permission';
import { RoleRoutePath } from './route-path/role';
import { UserRoutePath } from './route-path/user';
import { VehicleRoutePath } from './route-path/vehicle-route-path';

export const RoutePath = {
    AppRoutePath: [{ path: '', component: HomeComponent, pathMatch: 'full' },
      {path:'',  loadChildren: () =>import('src/app/featured/counter/counter.module').then(
        (m) => m.CounterModule
      )},
      {path:'', loadChildren: () =>import('src/app/featured/auth-callback/auth-callback.module').then(
        (m) => m.AuthCallbackModule
      )},
      {path:'', loadChildren: () =>import('src/app/core/dashboard/dashboard.module').then(
        (m) => m.DashboardModule
      ), data: { breadcrumb: 'Dashboard'}},
      { path:'forbidden',component: ForbiddenComponent, pathMatch:'full'},
      { path:'internal-server-error', component: InternalServerErrorComponent, pathMatch:'full'},
      { path: '**', component: UndefinedPageComponent, pathMatch: 'full' }
    ],

  CounterRoutePath:[{path: 'counter', component: CounterComponent}],
  DashboardRoutePath: [{path: 'dashboard', component: DashboardComponent, 
                      children: [RoleRoutePath,
                        PermissionRoutePath,
                        UserRoutePath,
                        VehicleRoutePath
                      ],
                      canActivate: [AuthGuard] }],
  AuthCallbackRoutePath:[{path: 'auth-callback', component: AuthCallbackComponent, canActivate: [AuthGuard]}],
  RoleRoutePath:[{path: '', component: RoleComponent}],
  PermissionRoutePath:[{path: '', component: PermissionComponent}],
  UserRoutePath: [{path: '', component: UserComponent}],
  UserCreateRoutePath: [{path: '', component: CreateComponent,resolve: {
    roleData: RoleResolverService
  }}],
  VehicleInventoryRoutePath: [{path: '', component: VehicleRegisterComponent}],
  VehicleInventoryCreateRoutePath: [{path: '', component: VehicleCreateComponent}],
  VehicleInventoryUpdateRoutePath: [{path: '', component: VehicleUpdateComponent}],
  VehicleMaintenanceRoutePath: [{path: '', component: MaintenanceComponent}],
  VehicleMaintenanceCreateRoutePath: [{path: '', component: CreatemaintenanceComponent}],
};