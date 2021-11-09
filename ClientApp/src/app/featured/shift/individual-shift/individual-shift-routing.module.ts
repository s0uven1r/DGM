import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from '../../../infrastructure/datum/route-component';
import { RoutePath } from '../../../infrastructure/datum/route-path';

const routes: Routes = RoutePath.IndividualShiftRoutePath;

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IndividualShiftRoutingModule { }
export const RoutingComponent = RouteComponent.IndividualShiftRouteComponent;
