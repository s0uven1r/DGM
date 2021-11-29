import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent,ShiftUpdateRoutingModule } from './shift-update-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { ShiftService } from '../../service/shift.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ShiftUpdateRoutingModule,
    CheckDirectiveModule
  ],
  providers: [
    {
      provide: ShiftService,
      useClass: ShiftService
    }
  ]
})
export class ShiftUpdateModule { }
