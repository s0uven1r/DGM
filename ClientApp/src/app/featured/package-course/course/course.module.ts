import { CourseService } from './../service/course.service';
import { CheckDirectiveModule } from './../../../shared/directives/checkclaim.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, CourseRoutingModule } from './course-routing.module';
import { DataTablesModule } from 'angular-datatables';


@NgModule({
  declarations: RoutingComponent,
  imports:[
    CommonModule,
    CourseRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [{provide: CourseService, useClass: CourseService}]
})
export class CourseModule { }
