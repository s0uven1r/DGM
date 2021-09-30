import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from 'src/app/infrastructure/datum/route-component';
import { RoutePath } from 'src/app/infrastructure/datum/route-path';

const routes: Routes = RoutePath.PromoUpdateRoutePath;

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PromoUpdateRoutingModule { }
export const RoutingComponent = RouteComponent.PromoUpdateRouteComponent;