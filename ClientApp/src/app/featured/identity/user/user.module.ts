import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables'
import { UserRoutingModule, RoutingComponent } from './user-routing.module';
import { UserService } from './service/user.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    UserRoutingModule,
    DataTablesModule
  ],
  providers: [ {
    provide: UserService,
    useClass: UserService
  },]
})
export class UserModule { }
