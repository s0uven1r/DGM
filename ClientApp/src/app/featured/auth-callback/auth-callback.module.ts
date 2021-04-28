import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthCallbackRoutingModule, RoutingComponent } from './auth-callback-routing.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    AuthCallbackRoutingModule
  ]
})
export class AuthCallbackModule { }
