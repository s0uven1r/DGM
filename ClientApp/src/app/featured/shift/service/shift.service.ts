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
  
  constructor(private http: HttpClient) {}

  getAllByAccountNumber(accountNo: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAllByAccountNumberURL}/${accountNo}`);
  }
  
}

