import { AppComponent } from "src/app/app.component";
import { HomeComponent } from "src/app/core/home/home.component";
import { CounterComponent } from "src/app/featured/counter/counter.component";
import { NavMenuComponent } from "src/app/core/nav-menu/nav-menu.component";
import { LoginComponent } from "src/app/featured/login/login.component";

export const RouteComponent = {
    AppRouteComponent: [
        AppComponent,
        HomeComponent,
        NavMenuComponent
      ],
      CounterRouteComponent: [
        CounterComponent
      ],
      LoginRouteComponent: [
        LoginComponent
      ]
}