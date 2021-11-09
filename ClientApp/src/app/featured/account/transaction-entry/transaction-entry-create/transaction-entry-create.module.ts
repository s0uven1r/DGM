import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, TransactionEntryCreateRoutingModule } from './transaction-entry-create-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../../service/account.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { UserService } from '../../../identity/user/service/user.service';
import { VehicleService } from '../../../vehicle/service/vehicle.service';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TransactionEntryCreateRoutingModule,
    NgbModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    },{
      provide: UserService,
      useClass: UserService,
    },{
      provide: VehicleService,
      useClass: VehicleService,
    },
  ]
})
export class TransactionEntryCreateModule { }