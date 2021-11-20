import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  RoutingComponent,
  ShiftCreateRoutingModule,
} from './shift-create-routing.module';
import { ShiftService } from '../../service/shift.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ShiftCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: ShiftService,
      useClass: ShiftService
    },
  ],
})
export class ShiftCreateModule { }
