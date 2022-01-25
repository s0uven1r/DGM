import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftFrequencyUpdateRoutingModule,RoutingComponent } from './shift-frequency-update-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { ShiftService } from '../../service/shift.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ShiftFrequencyUpdateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: ShiftService,
      useClass: ShiftService
    }
  ]
})

export class ShiftFrequencyUpdateModule { }
