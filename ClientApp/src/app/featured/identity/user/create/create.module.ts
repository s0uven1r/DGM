import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateRoutingModule, RoutingComponent } from './create-routing.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CreateRoutingModule
  ]
})
export class CreateModule { }
