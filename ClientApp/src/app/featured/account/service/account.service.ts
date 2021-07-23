import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class AccountService {
  private baseUrl = environment.resourceUrl;
  private accountHead = ApiGateway.resource.account.accounthead.base;
  private accountType = ApiGateway.resource.account.accounttype.base;
  private getAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.getAll;
  private getAccountTypeByIdUrl =
    this.accountType + ApiGateway.resource.account.accounttype.getSingleById;
  private createAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.create;
  private updateAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.update;
  private getAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.getAll;
  private getAccountHeadByIdUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.getSingleById;
  private createAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.create;
  private updateAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.update;
  constructor(private http: HttpClient) {}

  getAllAccountType(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountTypeUrl}`);
  }
  getByIdAccountType(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountTypeByIdUrl}`);
  }
  createAccountType(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createAccountTypeUrl}`, {
      title: value.title,
      type: value.type,
    });
  }
  updateAccountType(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updateAccountTypeUrl}/${value.id}`,
      {
        title: value.title,
        type: value.type,
      }
    );
  }
  getAllAccountHead(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountHeadUrl}`);
  }
  getByIdAccountHead(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountHeadByIdUrl}`);
  }
  createAccountHead(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createAccountHeadUrl}`, {
      title: value.title,
      accountType: value.accountType,
    });
  }
  updateAccountHead(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updateAccountHeadUrl}/${value.id}`,
      {
        title: value.title,
        accountType: value.accountType,
      }
    );
  }
}
