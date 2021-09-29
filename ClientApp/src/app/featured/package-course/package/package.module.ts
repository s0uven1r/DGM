import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { RoutingComponent, PackageRoutingModule } from './package-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { PackageService } from '../service/package.service';


@NgModule({
  declarations: 
    RoutingComponent,
  imports: [
    CommonModule,
    PackageRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [ {
    provide: PackageService,
    useClass: PackageService
  },]
})
export class PackageModule { }

