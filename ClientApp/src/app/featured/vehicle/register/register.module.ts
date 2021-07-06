import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { RoutingComponent, RegisterRoutingModule } from './register-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { VehicleService } from '../service/vehicle.service';


@NgModule({
  declarations: [
    RoutingComponent,
  ],
  imports: [
    CommonModule,
    RegisterRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [ {
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class RegisterModule { }

