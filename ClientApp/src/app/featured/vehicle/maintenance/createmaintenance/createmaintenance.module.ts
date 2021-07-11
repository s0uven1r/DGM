import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreatemaintenanceRoutingModule, RoutingComponent } from './createmaintenance-routing.module';
import { VehicleService } from '../../service/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CreatemaintenanceRoutingModule
  ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class CreatemaintenanceModule { }
