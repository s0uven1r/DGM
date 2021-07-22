import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissionRoutingModule, RoutingComponent } from './permission-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionService } from './service/permission.service';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';

@NgModule({
  imports: [
    CommonModule,
    PermissionRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    CheckDirectiveModule
  ],
  declarations: RoutingComponent,
  providers: [ {
    provide: PermissionService,
    useClass: PermissionService
  },]
})
export class PermissionModule { }
