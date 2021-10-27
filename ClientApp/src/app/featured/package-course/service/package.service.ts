import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class PackageService {
  private baseUrl = environment.resourceUrl;
  private packageUrl = ApiGateway.resource.package.package.base;
  private promoUrl = ApiGateway.resource.package.promo.base;
  private courseUrl = ApiGateway.resource.course.course.base;
  private shiftFrequencyUrl = ApiGateway.resource.shiftFrequency.base;

  private getAllPackageUrl = this.packageUrl + ApiGateway.resource.package.package.getAll;
  private getPackageByIdUrl = this.packageUrl+ ApiGateway.resource.package.package.getSingleById;
  private createPackageUrl = this.packageUrl + ApiGateway.resource.package.package.create;
  private updatePackageUrl = this.packageUrl + ApiGateway.resource.package.package.update;
  private deletePackageUrl = this.packageUrl + ApiGateway.resource.package.package.delete;

  private getAllPromoUrl = this.promoUrl + ApiGateway.resource.package.promo.getAll;
  private getPromoByIdUrl = this.promoUrl+ ApiGateway.resource.package.promo.getSingleById;
  private createPromoUrl = this.promoUrl + ApiGateway.resource.package.promo.create;
  private updatePromoUrl = this.promoUrl + ApiGateway.resource.package.promo.update;
  private deletePromoUrl = this.promoUrl + ApiGateway.resource.package.promo.delete;

  private getAllCourseUrl = this.courseUrl + ApiGateway.resource.course.course.getAll;

  private getAllShiftFrequencyUrl = this.shiftFrequencyUrl + ApiGateway.resource.shiftFrequency.getAll;

  constructor(private http: HttpClient) {}
  
  getCourse(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllCourseUrl}`);
  }

  getPackage(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllPackageUrl}`);
  }
  getSinglePackage(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getPackageByIdUrl}/${id}`);
  }
  createPackage(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createPackageUrl}`, {
      packageName: value.packageName,
      courseId: value.courseId,
      totalDay: value.totalDay,
      shiftFrequencyId: value.shiftFrequencyId,
      price: value.price,
    });
  }
  updatePackage(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updatePackageUrl}/${value.id}`,
      {
        packageName: value.packageName,
        courseId: value.courseId,
        totalDay: value.totalDay,
        shiftFrequencyId: value.shiftFrequencyId,
        price: value.price,
      }
    );
  }
  deletePackageById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deletePackageUrl}/${id}`
    );
  }

  getPromo(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllPromoUrl}`);
  }

  getSinglePromo(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getPromoByIdUrl}/${id}`);
  }

  createPromo(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createPromoUrl}`, {
      promoCode: value.promoCode,
      packageId: value.packageId,
      hasDiscountPercent: value.hasDiscountPercent,
      discount: value.discount,
      startDate: value.startDate,
      startDateNp: 
                value.startDateNp.day +
                "/" +
                value.startDateNp.month +
                "/" +
                value.startDateNp.year,
      endDate: value.endDate,
      endDateNp: 
                value.endDateNp.day +
                "/" +
                value.endDateNp.month +
                "/" +
                value.endDateNp.year,
    });
  }

  updatePromo(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updatePromoUrl}/${value.id}`,
      {
        promoCode: value.promoCode,
        packageId: value.packageId,
        hasDiscountPercent: value.hasDiscountPercent,
        discount: value.discount,
        startDate: value.startDate,
        startDateNp: 
                value.startDateNp.day +
                "/" +
                value.startDateNp.month +
                "/" +
                value.startDateNp.year,
        endDate: value.endDate,
        endDateNp: 
                value.endDateNp.day +
                "/" +
                value.endDateNp.month +
                "/" +
                value.endDateNp.year,
      }
    );
  }

  deletePromoById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deletePromoUrl}/${id}`
    );
  }

  getAllShiftFrequency(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllShiftFrequencyUrl}`);
  }
}
