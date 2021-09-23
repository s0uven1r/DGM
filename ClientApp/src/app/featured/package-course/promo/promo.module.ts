import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PromoRoutingModule, RoutingComponent } from './promo-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { PackageService } from '../service/package.service';
import { DataTablesModule } from 'angular-datatables';




@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    PromoRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [
    {
      provide: PackageService,
      useClass: PackageService,
    }
  ],
})
export class PromoModule { }
