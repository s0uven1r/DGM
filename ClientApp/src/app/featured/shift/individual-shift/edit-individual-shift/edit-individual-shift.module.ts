import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';
import { EditIndividualShiftRoutingModule, RoutingComponent } from './edit-individual-shift-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../../identity/user/service/user.service';
import { ShiftService } from '../../service/shift.service';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    EditIndividualShiftRoutingModule,
    CheckDirectiveModule,
    NgbModule,
    ReactiveFormsModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [
   {
      provide: UserService,
      useClass: UserService,
    },
    {
      provide: ShiftService,
      useClass: ShiftService
    }
  ]
})
export class EditIndividualShiftModule { }
