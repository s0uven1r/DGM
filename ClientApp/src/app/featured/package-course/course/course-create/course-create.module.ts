import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import {
  RoutingComponent,
  CourseCreateRoutingModule,
} from "./course-create-routing.module";
import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";

import { CourseTypeResolverService } from "../../service/course-type-resolver.service";
import { CourseService } from 'src/app/featured/package-course/service/course.service';

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CourseCreateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: CourseService,
      useClass: CourseService,
    },
    {
      provide: CourseTypeResolverService,
      useClass: CourseTypeResolverService
    }
  ],
})
export class CourseCreateModule { }
