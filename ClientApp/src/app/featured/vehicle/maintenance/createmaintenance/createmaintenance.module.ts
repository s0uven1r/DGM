import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreatemaintenanceRoutingModule, RoutingComponent } from './createmaintenance-routing.module';



@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CreatemaintenanceRoutingModule
  ]
})
export class CreatemaintenanceModule { }
