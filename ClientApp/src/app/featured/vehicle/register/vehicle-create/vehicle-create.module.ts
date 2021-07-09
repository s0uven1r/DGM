import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehicleCreateRoutingModule, RoutingComponent } from './vehicle-create-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { VehicleService } from '../../service/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: 
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehicleCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class VehicleCreateModule { }
