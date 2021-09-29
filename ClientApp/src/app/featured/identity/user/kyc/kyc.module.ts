import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KycComponent } from './kyc.component';
import { KycRoutingModule } from './kyc-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../service/user.service';



@NgModule({
  declarations: [
    KycComponent
  ],
  imports: [
    CommonModule,
    KycRoutingModule,
    ReactiveFormsModule
  ],
  providers: [UserService]
})
export class KycModule { }
