import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule, RoutingComponent } from './role-routing.module';
import { RoleService } from './service/role.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    RoleRoutingModule,
    ReactiveFormsModule,
    CheckDirectiveModule
  ],
  providers: [ {
    provide: RoleService,
    useClass: RoleService
  },]
})
export class RoleModule { }
