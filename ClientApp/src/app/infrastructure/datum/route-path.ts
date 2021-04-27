import { HomeComponent } from 'src/app/core/home/home.component';
import { UndefinedPageComponent } from 'src/app/core/undefined-page/undefined-page.component';
import { CounterComponent } from 'src/app/featured/counter/counter.component';
import { LoginComponent } from 'src/app/featured/login/login.component';

export const RoutePath = {
    AppRoutePath: [{ path: '', component: HomeComponent, pathMatch: 'full' },
    {path:'',  loadChildren: () =>import('src/app/featured/counter/counter.module').then(
      (m) => m.CounterModule
    )
    },
    {path:'', loadChildren: () =>import('src/app/featured/login/login.module').then(
        (m) => m.LoginModule
      )
      },
      { path: '**', component: UndefinedPageComponent, pathMatch: 'full' }
    ],
  CounterRoutePath:[{path: 'counter', component: CounterComponent}],
  LoginRoutePath:[{path: 'login', component: LoginComponent}]
};