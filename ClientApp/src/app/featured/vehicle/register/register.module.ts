import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RegisterRoutingModule } from './register-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RegisterRoutingModule,
    CheckDirectiveModule
  ]
})
export class RegisterModule { }
