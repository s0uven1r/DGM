import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftFrequencyCreateRoutingModule, RoutingComponent } from './shift-frequency-create-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ShiftService } from '../../service/shift.service';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ShiftFrequencyCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: ShiftService,
      useClass: ShiftService
    }
  ]
})
export class ShiftFrequencyCreateModule { }
