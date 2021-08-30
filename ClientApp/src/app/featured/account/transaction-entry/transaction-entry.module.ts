import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, TransactionEntryRoutingModule } from './transaction-entry-routing.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    TransactionEntryRoutingModule
  ]
})
export class TransactionEntryModule { }
