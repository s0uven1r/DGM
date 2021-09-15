import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RouteComponent } from 'src/app/infrastructure/datum/route-component';
import { RoutePath } from 'src/app/infrastructure/datum/route-path';

const routes: Routes = RoutePath.UserKycRoutePath;

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class KycRoutingModule { }
export const RoutingComponent = RouteComponent.KYCRouteComponent;
