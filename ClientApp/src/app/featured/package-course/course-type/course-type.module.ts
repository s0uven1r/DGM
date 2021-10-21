import { CourseTypeService } from '../service/course-type.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoutingComponent, CourseTypeRoutingModule } from './course-type-routing.module';
import { DataTablesModule } from 'angular-datatables';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    CourseTypeRoutingModule,
    CheckDirectiveModule,
    DataTablesModule,
  ],
  providers: [{provide: CourseTypeService, useClass: CourseTypeService}]
})
export class CourseTypeModule { }
