import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule, RoutingComponent } from './role-routing.module';
import { RoleService } from './service/role.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    RoleRoutingModule
  ],
  providers: [ {
    provide: RoleService,
    useClass: RoleService
  },]
})
export class RoleModule { }
