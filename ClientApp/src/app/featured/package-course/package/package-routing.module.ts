import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from 'src/app/infrastructure/datum/route-component';
import { RoutePath } from 'src/app/infrastructure/datum/route-path';

const routes: Routes = RoutePath.PackageRoutePath;

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PackageRoutingModule { }
export const RoutingComponent = RouteComponent.PackageRouteComponent;
