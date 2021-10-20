import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LogoRoutingModule, RoutingComponent } from './logo-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { LogoService } from '../service/logo.service';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: RoutingComponent,
  imports:[
    CommonModule,
    LogoRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
    ReactiveFormsModule,
  ],
  providers: [ {
    provide: LogoService,
    useClass: LogoService
  },]
})
export class LogoModule { }
