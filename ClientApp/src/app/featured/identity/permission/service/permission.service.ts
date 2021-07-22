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
    private checkPermissionUrl = ApiGateway.identity.permission.base  + ApiGateway.identity.permission.checkPermission;
    private claimsData:any [] = [];
    constructor(private http: HttpClient) { }
    getPermission(id: string): Observable<any>{
      return this.http.get<any>(`${this.baseUrl+ this.getPermissionUrl}/${id}`);
    }
    assignPermission(val: any):Observable<any>{
      val.modules.forEach(element => {
        var claims = element.claims.filter((x: { hasChecked: any; }) => x.hasChecked);
        claims.forEach(data => {
          this.claimsData.push(data);
        });
      });
      return this.http.post<any>(`${this.baseUrl+ this.postPermissionUrl}`, {roleId: val.roleId, claimList: this.claimsData});
    }
    checkPermission(claim: string[]):Observable<any>{
      return this.http.post<any>(`${this.baseUrl+ this.checkPermissionUrl}`, claim);
    }
}
