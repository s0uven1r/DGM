import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MaintenanceRoutingModule, RoutingComponent } from './maintenance-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { DataTablesModule } from 'angular-datatables';
import { VehicleService } from '../service/vehicle.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    MaintenanceRoutingModule,
    DataTablesModule,
    CheckDirectiveModule
  ],
  providers: [{
    provide: VehicleService,
    useClass: VehicleService
  },]
})
export class MaintenanceModule { }
