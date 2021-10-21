import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseTypeUpdateRoutingModule, RoutingComponent } from './course-type-update-routing.module';
import { CourseTypeService } from '../../service/course-type.service';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CourseTypeUpdateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: CourseTypeService,
      useClass: CourseTypeService,
    },
  ],
})
export class CourseTypeUpdateModule { }