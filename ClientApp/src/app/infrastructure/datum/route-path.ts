import { AuthGuard } from 'src/app/core/authorize/auth-guard';
import { DashboardComponent } from 'src/app/core/dashboard/dashboard.component';
import { HomeComponent } from 'src/app/core/home/home.component';
import { UndefinedPageComponent } from 'src/app/core/undefined-page/undefined-page.component';
import { AuthCallbackComponent } from 'src/app/featured/auth-callback/auth-callback.component';
import { CounterComponent } from 'src/app/featured/counter/counter.component';

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
      )},
      { path: '**', component: UndefinedPageComponent, pathMatch: 'full' }
    ],
  CounterRoutePath:[{path: 'counter', component: CounterComponent}],
  DashboardRoutePath: [{path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] }],
  AuthCallbackRoutePath:[{path: 'auth-callback', component: AuthCallbackComponent, canActivate: [AuthGuard]}],
};