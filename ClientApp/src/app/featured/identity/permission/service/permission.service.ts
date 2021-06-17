import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { environment } from 'src/environments/environment';

@Injectable()
export class PermissionService {

    private baseUrl = environment.apiIdentityUrl;
    private getPermissionUrl =   ApiGateway.identity.permission.base  + ApiGateway.identity.permission.getPermission;
    private postPermissionUrl = ApiGateway.identity.permission.base  + ApiGateway.identity.permission.managePermission;
    constructor(private http: HttpClient) { }
    getPermission(id: string): Observable<any>{
      return this.http.get<any>(`${this.baseUrl+ this.getPermissionUrl}/${id}`);
    }
    assignPermission(val: any):Observable<any>{
      var claimsData = val.claims.filter((x: { hasChecked: any; }) => x.hasChecked);
      return this.http.post<any>(`${this.baseUrl+ this.postPermissionUrl}`, {roleId: val.roleId, claimList: claimsData});
    }
}
