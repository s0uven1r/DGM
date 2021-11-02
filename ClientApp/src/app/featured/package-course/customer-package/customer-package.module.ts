import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomerPackageRoutingModule, RoutingComponent, } from './customer-package-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';
import { PackageService } from '../service/package.service';
@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CustomerPackageRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
    ReactiveFormsModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [ {
    provide: PackageService,
    useClass: PackageService
  },]
})
export class CustomerPackageModule { }
