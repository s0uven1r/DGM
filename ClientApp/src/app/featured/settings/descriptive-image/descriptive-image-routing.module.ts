import { RoutePath } from './../../../infrastructure/datum/route-path';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from './../../../infrastructure/datum/route-component';

const routes: Routes = RoutePath.DescriptiveImageRoutePath;
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DescriptiveImageRoutingModule { }
export const RoutingComponent = RouteComponent.DescriptiveImageRouteComponent;
