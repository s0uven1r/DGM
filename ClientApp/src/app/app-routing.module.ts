import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from './infrastructure/datum/route-component';
import { RoutePath } from './infrastructure/datum/route-path';

const routes: Routes = RoutePath.AppRoutePath;

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const RoutingComponent = RouteComponent.AppRouteComponent;
