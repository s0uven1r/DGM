import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, PackageUpdateRoutingModule } from './package-update-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { ReactiveFormsModule } from '@angular/forms';
import { PackageService } from '../../service/package.service';

@NgModule({
  declarations:
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PackageUpdateRoutingModule,
    CheckDirectiveModule
  ],
  providers: [{
    provide: PackageService,
    useClass: PackageService
  },]
})
export class PackageUpdateModule { }


