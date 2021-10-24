import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CourseTypeCreateRoutingModule, RoutingComponent } from './course-type-create-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckDirectiveModule } from 'src/app/shared/directives/checkclaim.module';
import { CourseTypeService } from '../../service/course-type.service';


@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CourseTypeCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: CourseTypeService,
      useClass: CourseTypeService,
    },
  ]
})
export class CourseTypeCreateModule { }
