import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseTypeCreateRoutingModule } from './course-type-create-routing.module';
import { CourseTypeCreateComponent } from './course-type-create/course-type-create.component';


@NgModule({
  declarations: [
    CourseTypeCreateComponent
  ],
  imports: [
    CommonModule,
    CourseTypeCreateRoutingModule
  ]
})
export class CourseTypeCreateModule { }
