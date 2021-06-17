import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateRoutingModule, RoutingComponent } from './create-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../service/user.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CreateRoutingModule,
    ReactiveFormsModule
  ],
  providers: [UserService]
})
export class CreateModule { }
