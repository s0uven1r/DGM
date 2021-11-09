import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, TransactionEntryRoutingModule } from './transaction-entry-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { AccountService } from '../service/account.service';

import { DataTablesModule } from 'angular-datatables';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    TransactionEntryRoutingModule,
    CheckDirectiveModule,
    DataTablesModule
  ],
  providers: [
    {
      provide: AccountService,
      useClass: AccountService,
    }
  ]
})
export class TransactionEntryModule { }
