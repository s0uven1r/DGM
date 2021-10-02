import { RoutePath } from './../../../infrastructure/datum/route-path';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from './../../../infrastructure/datum/route-component';

const routes: Routes = RoutePath.CourseRoutePath;
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CourseRoutingModule { }
export const RoutingComponent = RouteComponent.CourseRouteComponent;
