import { HttpClient } from "@angular/common/http";
import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: 'root'
})
export class CourseTypeService {
  private  baseUrl = environment.resourceUrl;
  private  courseUrl = ApiGateway.resource.course.courseType.base;
  
  private  getAllCourseTypeUrl = this.courseUrl + ApiGateway.resource.course.courseType.getAll;
  private  createCourseTypeUrl = this.courseUrl + ApiGateway.resource.course.courseType.create;
  private  updateCourseTypeUrl = this.courseUrl + ApiGateway.resource.course.courseType.update;
  private  deleteCourseTypeUrl = this.courseUrl + ApiGateway.resource.course.courseType.delete;
  private getCourseTypeByIdUrl = this.courseUrl+ ApiGateway.resource.course.courseType.getSingleById;

  constructor(private http: HttpClient) { }

  getCourseType(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllCourseTypeUrl}`);
  }
  getSingleCourseType(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getCourseTypeByIdUrl}/${id}`);
  }
  createCourseType(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createCourseTypeUrl}`, value);
  }
  deleteCourseTypeById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deleteCourseTypeUrl}/${id}`
    );
  }
  UpdateCourseType(value: any) {
    return this.http.put<any>(`${this.baseUrl + this.updateCourseTypeUrl}/${value.id}`, value);
  }
}
