import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class ShiftService {
  private  baseUrl = environment.resourceUrl;

  private  individualShiftUrl = ApiGateway.resource.shift.individualShift.base;
  private  getAllByAccountNumberURL = this.individualShiftUrl + ApiGateway.resource.shift.individualShift.getAllByAccountNumber;
  private getIndividualShiftByIdUrl = this.individualShiftUrl + ApiGateway.resource.shift.individualShift.getSingleById;
  private UpdateIndividualShiftUrl =  this.individualShiftUrl + ApiGateway.resource.shift.individualShift.update;
  constructor(private http: HttpClient) {}

  getAllByAccountNumber(accountNo: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllByAccountNumberURL}/${accountNo}`);
  }
  getSingleIndividualShift(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getIndividualShiftByIdUrl}/${id}`);
  }
  UpdateIndividualShift(value: any){
    return this.http.put<any>(`${this.baseUrl + this.UpdateIndividualShiftUrl}/${value.id}`,  {
      shiftId: value.shiftId,
      vehicleId: value.vehicleId,
      trainerId: value.accountNumber,
      trainerDetail: value.trainerName,
      trainingDate: value.trainingDateEN,
      trainingDateNp: value.trainingDateNP.day +"/" + value.trainingDateNP.month + "/" + value.trainingDateNP.year
    });
  }
}

