import { AuthGuard } from 'src/app/core/authorize/auth-guard';
import { HomeComponent } from 'src/app/core/home/home.component';
import { UndefinedPageComponent } from 'src/app/core/undefined-page/undefined-page.component';
import { AuthCallbackComponent } from 'src/app/featured/auth-callback/auth-callback.component';
import { CounterComponent } from 'src/app/featured/counter/counter.component';

export const RoutePath = {
    AppRoutePath: [{ path: '', component: HomeComponent, pathMatch: 'full' },
    {path:'',  loadChildren: () =>import('src/app/featured/counter/counter.module').then(
      (m) => m.CounterModule
    )
    },
      {path:'', loadChildren: () =>import('src/app/featured/auth-callback/auth-callback.module').then(
        (m) => m.AuthCallbackModule
      ), canActivate: [AuthGuard]
      },
      { path: '**', component: UndefinedPageComponent, pathMatch: 'full' }
    ],
  CounterRoutePath:[{path: 'counter', component: CounterComponent}],
  AuthCallbackRoutePath:[{path: 'auth-callback', component: AuthCallbackComponent}],
};