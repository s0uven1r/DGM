import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PermissionRoutingModule, RoutingComponent } from './permission-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { PermissionService } from './service/permission.service';

@NgModule({
  imports: [
    CommonModule,
    PermissionRoutingModule,
    ReactiveFormsModule
  ],
  declarations: RoutingComponent,
  providers: [ {
    provide: PermissionService,
    useClass: PermissionService
  },]
})
export class PermissionModule { }
