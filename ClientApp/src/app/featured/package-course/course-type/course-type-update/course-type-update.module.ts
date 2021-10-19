import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseTypeUpdateRoutingModule } from './course-type-update-routing.module';
import { CourseTypeUpdateComponent } from './course-type-update.component';


@NgModule({
  declarations: [
    CourseTypeUpdateComponent
  ],
  imports: [
    CommonModule,
    CourseTypeUpdateRoutingModule
  ]
})
export class CourseTypeUpdateModule { }
