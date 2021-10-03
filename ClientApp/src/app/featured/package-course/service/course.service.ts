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
  private deleteCourseUrl =  this.courseUrl + ApiGateway.resource.course.course.delete;
  private UpdateCourseUrl =  this.courseUrl + ApiGateway.resource.course.course.update;
  private getCourseByIdUrl = this.courseUrl+ ApiGateway.resource.course.course.getSingleById;

  constructor(private http: HttpClient) {}

  getCourse(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllCourseUrl}`);
  }
  
  getSingleCourse(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getCourseByIdUrl}/${id}`);
  }
  createCourse(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createCourseUrl}`, value);
  }
  deleteCourseById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deleteCourseUrl}/${id}`
    );
  }
  UpdateCourse(value: any) {
    return this.http.put<any>(`${this.baseUrl + this.UpdateCourseUrl}/${value.id}`, value);
  }
}

