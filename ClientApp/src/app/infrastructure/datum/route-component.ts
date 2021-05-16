import { AppComponent } from "src/app/app.component";
import { HomeComponent } from "src/app/core/home/home.component";
import { CounterComponent } from "src/app/featured/counter/counter.component";
import { AuthCallbackComponent } from "src/app/featured/auth-callback/auth-callback.component";
import { DashboardComponent } from "src/app/core/dashboard/dashboard.component";

export const RouteComponent = {
    AppRouteComponent: [
        AppComponent,
        HomeComponent,
        DashboardComponent
      ],
      CounterRouteComponent: [
        CounterComponent
      ],
      AuthCallbackRouteComponent: [
        AuthCallbackComponent
      ],
      DashBoardRouteComponent: []
      
}