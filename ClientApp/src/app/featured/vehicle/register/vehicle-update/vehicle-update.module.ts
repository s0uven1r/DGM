import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VehicleUpdateRoutingModule, RoutingComponent } from './vehicle-update-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { VehicleService } from '../../service/vehicle.service';
import { ReactiveFormsModule } from '@angular/forms';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';

@NgModule({
  declarations:
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    VehicleUpdateRoutingModule,
    CheckDirectiveModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class VehicleUpdateModule { }


