import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftFrequencyCreateRoutingModule } from './shift-frequency-create-routing.module';
import { ShiftFrequencyCreateComponent } from './shift-frequency-create.component';


@NgModule({
  declarations: [
    ShiftFrequencyCreateComponent
  ],
  imports: [
    CommonModule,
    ShiftFrequencyCreateRoutingModule
  ]
})
export class ShiftFrequencyCreateModule { }
