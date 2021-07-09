import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehicleUpdateRoutingModule, RoutingComponent } from './vehicle-update-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { VehicleService } from '../../service/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: 
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehicleUpdateRoutingModule,
    CheckDirectiveModule
  ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class VehicleUpdateModule { }


