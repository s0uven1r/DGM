import { NgNepaliDateDirectiveModule } from './../../../../shared/directives/attributes/ngNepaliDateDirective.module';
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehicleCreateRoutingModule, RoutingComponent } from './vehicle-create-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { VehicleService } from '../../service/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
@NgModule({
  declarations:
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehicleCreateRoutingModule,
    CheckDirectiveModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class VehicleCreateModule { }

