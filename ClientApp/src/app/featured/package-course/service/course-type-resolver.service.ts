import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { CourseTypeModel } from "src/app/infrastructure/model/UserManagement/resource/course/coursetypemodel";
import { CourseTypeService } from "./course-type.service";

@Injectable({
  providedIn: "root",
})
export class CourseTypeResolverService
  implements Resolve<Observable<CourseTypeModel[]>>
{
  constructor(private courseTypeService: CourseTypeService) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    return this.courseTypeService.getCourseType();
  }
}