import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule, RoutingComponent } from './login-routing.module';
import { HttpClientModule } from '@angular/common/http';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    HttpClientModule,
    LoginRoutingModule
  ]
})
export class LoginModule { }
