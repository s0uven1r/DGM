import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoRoutingModule, RoutingComponent } from './logo-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';



@NgModule({
  declarations: RoutingComponent,
  imports:[
    CommonModule,
    LogoRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ]
})
export class LogoModule { }
