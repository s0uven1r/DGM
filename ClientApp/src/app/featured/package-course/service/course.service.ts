import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class CourseService {
  private  baseUrl = environment.resourceUrl;
  private  courseUrl = ApiGateway.resource.course.course.base;


  private  getAllCourseUrl = this.courseUrl + ApiGateway.resource.course.course.getAll;
  private  createCourseUrl = this.courseUrl + ApiGateway.resource.course.course.create;

  constructor(private http: HttpClient) {}

  getCourse(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllCourseUrl}`);
  }
  createCourse(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createCourseUrl}`, {
      packageName: value.packageName,
      courseId: value.courseId,
      totalDay: value.totalDay,
      duration: value.duration,
      price: value.price,
    });
  }
}

