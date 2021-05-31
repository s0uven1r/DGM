import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { environment } from 'src/environments/environment';

@Injectable()
export class RoleService {
  private baseUrl = environment.apiIdentityUrl;
  private getRoleUrl = ApiGateway.identity.role.base + ApiGateway.identity.role.getRole;
  private postUrl  = ApiGateway.identity.role.base + ApiGateway.identity.role.postRole;
  constructor(private http: HttpClient) { }
  getRole(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getRoleUrl}`);
  }
  registerRole(title:string): Observable<any>{
   return this.http.post<any>(`${this.baseUrl + this.postUrl}`, { name : title})
  }
}
