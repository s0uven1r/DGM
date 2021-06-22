import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables'
import { UserRoutingModule, RoutingComponent } from './user-routing.module';
import { UserService } from './service/user.service';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    UserRoutingModule,
    DataTablesModule,
    CheckDirectiveModule
  ],
  providers: [ {
    provide: UserService,
    useClass: UserService
  },]
})
export class UserModule { }
