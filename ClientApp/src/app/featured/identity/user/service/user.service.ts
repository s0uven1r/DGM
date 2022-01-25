import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { UserKycModel } from "src/app/infrastructure/model/UserManagement/user-kyc-model";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private baseUrl = environment.apiIdentityUrl;
  private getUserUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.getUser;
  private enableUserUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.enableUser;
  private disableUserUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.disableUser;
  private userRegistrationUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.createEmployee;
  private getAccountDetailsUrl =
    ApiGateway.identity.user.base +
    ApiGateway.identity.user.getAccountNumberDetails;
    private getAccountTrainerDetailsUrl = ApiGateway.identity.user.base +
    ApiGateway.identity.user.getAccountTrainerDetails;
  private userUpdateUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.updateEmployee;
  private updateKycUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.updateKyc;
    private getKycDDLUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.getKycDDL;
    private getKycDetailUrl =
    ApiGateway.identity.user.base + ApiGateway.identity.user.getKycDetail;
  constructor(private http: HttpClient) {}

  getUser(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getUserUrl}`);
  }
  enableDisableLogin(id: string, isActivate: boolean): Observable<any> {
    if (isActivate) {
      return this.http.get<any>(`${this.baseUrl + this.enableUserUrl}/${id}`);
    } else {
      return this.http.get(`${this.baseUrl + this.disableUserUrl}/${id}`);
    }
  }
  performInternalUserAction(value: any) {
    if (value.id) {
      return this.http.put<any>(`${this.baseUrl + this.userUpdateUrl}`, {
        id: value.id,
        firstName: value.firstName,
        middleName: value.middleName,
        lastName: value.lastName,
        phone: value.phone,
        roleId: value.appliedRole,
      });
    }
    return this.http.post<any>(`${this.baseUrl + this.userRegistrationUrl}`, {
      firstName: value.firstName,
      middleName: value.middleName,
      lastName: value.lastName,
      email: value.email,
      confirmEmail: value.confirmEmail,
      confirmPassword: value.phone,
      phone: value.phone,
      userName: value.email,
      password: "Dgm" + value.phone,
      roleId: value.appliedRole,
    });
  }
  getUserById(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getUserUrl}/${id}`);
  }
  getAllAccountDetails(name: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl + this.getAccountDetailsUrl}?value=${name}`
    );
  }
  getAllAccountTrainerDetails(name: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl + this.getAccountTrainerDetailsUrl}?value=${name}`
    );
  }
  updateKyc(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.updateKycUrl}`, {
      fullname: value.fullname,
      permanentAddress: value.permanentAddress,
      temporaryAddress: value.temporaryAddress,
      contactNumber: value.contactNumber,
      alternativeContactNumber: value.alternativeContactNumber,
      citizenshipNumber: value.citizenshipNumber,
      panNumber: value.panNumber,
      gender: value.gender,
      maritalStatus: value.maritalStatus,
      fathersName: value.fathersName,
      mothersName: value.mothersName,
      spouseName: value.spouseName,
      child1Name: value.child1Name,
      child2Name: value.child2Name,
      bloodGroup: value.bloodGroup,
      anyMedication: value.anyMedication,
      anyMedicalCondition: value.anyMedicalCondition,
    });
  }
  
  getKYCDDL(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getKycDDLUrl}`);
  }

  getKYCDetail(id :string): Observable<UserKycModel> {
    return this.http.get<UserKycModel>(`${this.baseUrl + this.getKycDetailUrl}/${id}`);
  }
}
