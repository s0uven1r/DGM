import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import {
  RoutingComponent,
  CourseUpdateRoutingModule,
} from "./course-update-routing.module";
import { CheckDirectiveModule } from "src/app/shared/directives/checkclaim.module";
import { CourseService } from "../../service/course.service";

@NgModule({
  declarations: RoutingComponent,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CourseUpdateRoutingModule,
    CheckDirectiveModule,
  ],
  providers: [
    {
      provide: CourseService,
      useClass: CourseService,
    },
  ],
})
export class CourseUpdateModule {}
