import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateRoutingModule, RoutingComponent } from './create-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../service/user.service';
import { BlockCopyPasteModule } from 'src/app/shared/directives/blockcopypaste.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    BlockCopyPasteModule,
    CreateRoutingModule,
    ReactiveFormsModule
  ],
  providers: [UserService]
})
export class CreateModule { }
