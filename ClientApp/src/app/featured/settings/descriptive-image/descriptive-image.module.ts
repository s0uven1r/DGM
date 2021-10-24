import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DescriptiveImageRoutingModule, RoutingComponent } from './descriptive-image-routing.module';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { LogoService } from '../service/logo.service';



@NgModule({
  declarations: RoutingComponent,
  imports:[
    CommonModule,
    DescriptiveImageRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [ {
    provide: LogoService,
    useClass: LogoService
  },]
})
export class DescriptiveImageModule { }
