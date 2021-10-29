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
  private transactionEntry = ApiGateway.resource.account.transactionentry.base;

  private getAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.getAll;
  private getAccountTypeByIdUrl =
    this.accountType + ApiGateway.resource.account.accounttype.getSingleById;
  private getAccountTypeDDLUrl =
    this.accountType + ApiGateway.resource.account.accounttype.getAccountTypeDDL;
  private createAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.create;
  private updateAccountTypeUrl =
    this.accountType + ApiGateway.resource.account.accounttype.update;
  private getAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.getAll;
  private getAccountHeadAccountDetailsUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.getAccountNumberDetails;
  private getAccountHeadByIdUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.getSingleById;
  private createAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.create;
  private updateAccountHeadUrl =
    this.accountHead + ApiGateway.resource.account.accounthead.update;
  private createTransactionEntryUrl =
    this.transactionEntry + ApiGateway.resource.account.transactionentry.create;
  private getTransactionEntryUrl =
    this.transactionEntry + ApiGateway.resource.account.transactionentry.getSingle;

  constructor(private http: HttpClient) { }

  getAllAccountType(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountTypeUrl}`);
  }
  getByIdAccountType(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountTypeByIdUrl}/${id}`);
  }
  getAccountTypeDDL(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountTypeDDLUrl}`);
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
      }
    );
  }
  getAllAccountHead(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountHeadUrl}`);
  }
  getByIdAccountHead(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountHeadByIdUrl}/${id}`);
  }
  createAccountHead(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createAccountHeadUrl}`, {
      title: value.title,
      accountTypeId: value.accountTypeId,
    });
  }
  updateAccountHead(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updateAccountHeadUrl}/${value.id}`,
      {
        title: value.title,
      }
    );
  }
  getAllAccountHeadAccountDetails(name: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountHeadAccountDetailsUrl}?value=${name}`);
  }
  createTransactionEntry(value: any) {
    value.journalEntry.forEach(element => {
      let date = element.entryDateNP.day + "/" + element.entryDateNP.month + "/" + element.entryDateNP.year;
      element.entryDateNP = date;
    });

    return this.http.post<any>(`${this.baseUrl + this.createTransactionEntryUrl}`, {
      accountNumber: value.accountNumber,
      discountAmount: value.discountAmount,
      dueAmount: value.dueAmount,
      entryDateEN: value.entryDateEN,
      marketPrice: value.marketPrice,
      netAmount: value.netAmount,
      remarks: value.remarks,
      title: value.title,
      type: value.type,
      entryDateNP:
        value.entryDateNP.day +
        "/" +
        value.entryDateNP.month +
        "/" +
        value.entryDateNP.year,
      journalEntries: value.journalEntry,
    });
  }
  getTransactionEntry(type: string, accountNumber: string, transactionDateEN: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getTransactionEntryUrl}?type=${type}&accountNumber=${accountNumber}&transactionDateEN=${transactionDateEN}`);
  }
}
