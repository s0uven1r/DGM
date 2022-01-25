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
  private identityBaseUrl = environment.apiIdentityUrl;
  private packageUrl = ApiGateway.resource.package.package.base;
  private promoUrl = ApiGateway.resource.package.promo.base;
  private courseUrl = ApiGateway.resource.course.course.base;
  private shiftFrequencyUrl = ApiGateway.resource.shiftFrequency.base;
  private shiftUrl = ApiGateway.resource.shift.base;

  private userUrl = ApiGateway.identity.user.base;
  private registerCustomerPackageUrl = this.userUrl + ApiGateway.identity.user.registerCustomerPackage;

  private getAllPackageUrl = this.packageUrl + ApiGateway.resource.package.package.getAll;
  private getPackageByIdUrl = this.packageUrl+ ApiGateway.resource.package.package.getSingleById;
  private createPackageUrl = this.packageUrl + ApiGateway.resource.package.package.create;
  private updatePackageUrl = this.packageUrl + ApiGateway.resource.package.package.update;
  private deletePackageUrl = this.packageUrl + ApiGateway.resource.package.package.delete;
  
  private getAllPromoUrl = this.promoUrl + ApiGateway.resource.package.promo.getAll;
  private getPromoByIdUrl = this.promoUrl+ ApiGateway.resource.package.promo.getSingleById;
  private getPromoByPromoCodeUrl = this.promoUrl+ ApiGateway.resource.package.promo.getSingleByPromoCode;
  private createPromoUrl = this.promoUrl + ApiGateway.resource.package.promo.create;
  private updatePromoUrl = this.promoUrl + ApiGateway.resource.package.promo.update;
  private deletePromoUrl = this.promoUrl + ApiGateway.resource.package.promo.delete;

  private getAllCourseUrl = this.courseUrl + ApiGateway.resource.course.course.getAll;

  private getAllShiftFrequencyUrl = this.shiftFrequencyUrl + ApiGateway.resource.shiftFrequency.getAll;

  private getAllShiftUrl = this.shiftUrl + ApiGateway.resource.shift.getAll;
  
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
      description: value.description
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
        description: value.description
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
  getSinglePromoByPromocode(promoCode: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getPromoByPromoCodeUrl}/${promoCode}`);
  }
  
  createPromo(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createPromoUrl}`, {
      promoCode: value.promoCode,
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

  updatePromo(value: any, startDate: string, endDate: string) {
    if(!value['endDateNp']){
      value['endDateNp'] = endDate;
      value['startDateNp'] = startDate;
    }else{
      value['endDateNp'] =  value.endDateNp.day + "/" + value.endDateNp.month + "/" + value.endDateNp.year;
      value['startDateNp'] = value.startDateNp.day + "/" + value.startDateNp.month + "/" + value.startDateNp.year;
    }
    return this.http.put<any>(
      `${this.baseUrl + this.updatePromoUrl}/${value.id}`,
      {
        promoCode: value.promoCode,
        hasDiscountPercent: value.hasDiscountPercent,
        discount: value.discount,
        startDate: value.startDate,
        startDateNp: value.startDateNp,
        endDate: value.endDate,
        endDateNp: value.endDateNp,
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

  getAllShift(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllShiftUrl}`);
  }
  registerCustomerPackage(value: any) {
    return this.http.post<any>(`${this.identityBaseUrl + this.registerCustomerPackageUrl}`, {
      "customerDetail": {
        "firstName": value['firstName'],
        "middleName": value['middleName'],
        "lastName": value['lastName'],
        "userName": value['email'],
        "email": value['email']
      },
      "packageId": value['packageId'],
      "startDate": value['startDateEN'],
      "startDateNP":  value.startDateNP.day +"/" +value.startDateNP.month + "/" + value.startDateNP.year,
      "endDate": value['endDateEN'],
      "endDateNP": value.endDateNP.day +"/" +value.endDateNP.month + "/" + value.endDateNP.year,
      "paymentGateway": 0,
      "shiftId": value['shiftId'],
      "paidAmount": value['paidAmount'],
      "promoCode": value['promoCode'],
      "isAdmin": true,
      "address": value['address'],
      "phoneNumber": value['phoneNumber'],
    });
  }
  
}
