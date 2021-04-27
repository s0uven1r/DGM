import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CounterRoutingModule, RoutingComponent } from './counter-routing.module';
import { CounterService } from './service/counter.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CounterRoutingModule
  ],
  providers: [ { provide: CounterService, useClass: CounterService}]
})
export class CounterModule { }

