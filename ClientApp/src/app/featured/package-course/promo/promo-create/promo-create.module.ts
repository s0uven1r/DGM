import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { PackageService } from '../../service/package.service';
import { PromoCreateRoutingModule, RoutingComponent } from './promo-create-routing.module';
import { NgNepaliDateDirectiveModule } from 'src/app/shared/directives/attributes/ngNepaliDateDirective.module';
import { NpDatepickerModule } from 'angular-nepali-datepicker';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  declarations:
    RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PromoCreateRoutingModule,
    CheckDirectiveModule,
    NgbModule,
    NpDatepickerModule,
    NgNepaliDateDirectiveModule
  ],
  providers: [{
    provide: PackageService,
    useClass: PackageService
  },]
})
export class PromoCreateModule { }
