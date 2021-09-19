import { Injectable } from "@angular/core";
import {
  Resolve,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { Observable } from "rxjs";
import { CourseModel } from "src/app/infrastructure/model/UserManagement/resource/course/coursemodel";
import { PackageService } from "./package.service";

@Injectable({
  providedIn: "root",
})
export class PackageResolverService
  implements Resolve<Observable<CourseModel[]>>
{
  constructor(private packageService: PackageService) {}
  resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot) {
    return this.packageService.getCourse();
  }
}
