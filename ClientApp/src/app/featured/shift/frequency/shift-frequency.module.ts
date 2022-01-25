import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShiftFrequencyRoutingModule, RoutingComponent } from './shift-frequency-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ShiftService } from '../service/shift.service';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CheckDirectiveModule,
    DataTablesModule,
    NgbModule,
    ShiftFrequencyRoutingModule
  ],
  providers: [
     {
       provide: ShiftService,
       useClass: ShiftService
     }
   ]
})
export class ShiftFrequencyModule { }
