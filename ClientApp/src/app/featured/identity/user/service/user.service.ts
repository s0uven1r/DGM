import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiIdentityUrl;
  private getUserUrl = ApiGateway.identity.user.base + ApiGateway.identity.user.getUser;
  private enableUserUrl = ApiGateway.identity.user.base + ApiGateway.identity.user.enableUser;
  private disableUserUrl = ApiGateway.identity.user.base + ApiGateway.identity.user.disableUser;

constructor(private http: HttpClient) { }

getUser(): Observable<any>{
  return this.http.get<any>(`${this.baseUrl + this.getUserUrl}`);
}
enableDisableLogin(id: string, isActivate: boolean): Observable<any>{
  if(isActivate){
    return this.http.get<any>(`${this.baseUrl + this.enableUserUrl}/${id}`);
  }else{
    return this.http.get(`${this.baseUrl + this.disableUserUrl}/${id}`);
  }
}
}

