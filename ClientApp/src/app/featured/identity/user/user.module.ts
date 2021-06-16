import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables'
import { UserRoutingModule, RoutingComponent } from './user-routing.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    UserRoutingModule,
    DataTablesModule
  ]
})
export class UserModule { }
