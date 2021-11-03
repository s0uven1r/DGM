import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from '../../../../infrastructure/datum/route-component';
import { RoutePath } from '../../../../infrastructure/datum/route-path';

const routes: Routes = RoutePath.IndividualShiftEditRoutePath;

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EditIndividualShiftRoutingModule { }
export const RoutingComponent = RouteComponent.IndividualShiftEditRouteComponent;
