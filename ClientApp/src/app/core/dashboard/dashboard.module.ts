import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule, RoutingComponent } from './dashboard-routing.module';
import { NavigationService } from './services';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    DashboardRoutingModule
  ], 
  providers: [ {

    provide: NavigationService,

    useClass: NavigationService

  }],
})
export class DashboardModule { }
