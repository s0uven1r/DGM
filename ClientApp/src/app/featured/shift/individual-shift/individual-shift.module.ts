import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { IndividualShiftRoutingModule, RoutingComponent } from './individual-shift-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../identity/user/service/user.service';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CheckDirectiveModule,
    DataTablesModule,
    NgbModule,
    IndividualShiftRoutingModule
  ],
  providers: [
   {
      provide: UserService,
      useClass: UserService,
    }
  ]
})
export class IndividualShiftModule { }
