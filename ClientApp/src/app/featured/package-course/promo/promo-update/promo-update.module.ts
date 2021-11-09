import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { PackageService } from '../../service/package.service';
import { PromoUpdateRoutingModule, RoutingComponent } from './promo-update-routing.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: 
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PromoUpdateRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
    NgbModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [ {
    provide: PackageService,
    useClass: PackageService
  },]
})
export class PromoUpdateModule { }
