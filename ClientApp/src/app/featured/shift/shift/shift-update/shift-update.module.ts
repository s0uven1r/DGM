import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftUpdateRoutingModule } from './shift-update-routing.module';
import { ShiftUpdateComponent } from './shift-update.component';


@NgModule({
  declarations: [
    ShiftUpdateComponent
  ],
  imports: [
    CommonModule,
    ShiftUpdateRoutingModule
  ]
})
export class ShiftUpdateModule { }
