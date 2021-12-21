import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShiftRoutingModule, RoutingComponent } from './shift-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { ShiftService } from '../service/shift.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ShiftRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
    NgbModule
  ],
  providers: [
    {
      provide: ShiftService,
      useClass: ShiftService
    }
  ]
})
export class ShiftModule { }
