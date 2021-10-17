import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { PackageService } from '../../service/package.service';
import { PromoUpdateRoutingModule, RoutingComponent } from './promo-update-routing.module';

@NgModule({
  declarations: 
    RoutingComponent,
  imports: [
    CommonModule,
    PromoUpdateRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [ {
    provide: PackageService,
    useClass: PackageService
  },]
})
export class PromoUpdateModule { }
