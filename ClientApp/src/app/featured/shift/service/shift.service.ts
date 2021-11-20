import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class ShiftService {
  private baseUrl = environment.resourceUrl;

  private shiftUrl = ApiGateway.resource.shift.base;
  private getAllShiftUrl = this.shiftUrl + ApiGateway.resource.shift.getAll;
  private getShiftByIdUrl = this.shiftUrl+ ApiGateway.resource.shift.getSingleById; 
  private createShiftUrl = this.shiftUrl + ApiGateway.resource.shift.create;
  private UpdateShiftUrl =  this.shiftUrl + ApiGateway.resource.shift.update;
  private deleteShiftUrl =  this.shiftUrl + ApiGateway.resource.shift.delete;



  private individualShiftUrl = ApiGateway.resource.individualShift.base;
  private getAllByAccountNumberURL = this.individualShiftUrl + ApiGateway.resource.individualShift.getAllByAccountNumber;
  private getIndividualShiftByIdUrl = this.individualShiftUrl + ApiGateway.resource.individualShift.getSingleById;
  private UpdateIndividualShiftUrl = this.individualShiftUrl + ApiGateway.resource.individualShift.update;
  constructor(private http: HttpClient) { }

  getAllShift(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllShiftUrl}`);
  }

  getSingleShift(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getShiftByIdUrl}/${id}`);
  }
  
  createShift(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createShiftUrl}`, value);
  }
  
  UpdateShift(value: any) {
    return this.http.put<any>(`${this.baseUrl + this.UpdateShiftUrl}/${value.id}`, value);
  }

  deleteShiftById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deleteShiftUrl}/${id}`
    );
  }
  

  getAllByAccountNumber(accountNo: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllByAccountNumberURL}/${accountNo}`);
  }
  getSingleIndividualShift(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getIndividualShiftByIdUrl}/${id}`);
  }
  UpdateIndividualShift(value: any) {
    return this.http.put<any>(`${this.baseUrl + this.UpdateIndividualShiftUrl}/${value.id}`, {
      shiftId: value.shiftId,
      vehicleId: value.vehicleId,
      trainerId: value.accountNumber,
      trainerDetail: value.trainerName,
      trainingDate: value.trainingDateEN,
      trainingDateNp: value.trainingDateNP.day + "/" + value.trainingDateNP.month + "/" + value.trainingDateNP.year
    });
  }
}

