import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  private baseUrl = environment.apiIdentityUrl;
  private getRoleUrl = ApiGateway.identity.role.base + ApiGateway.identity.role.getRole;
  private postUrl  = ApiGateway.identity.role.base + ApiGateway.identity.role.postRole;
  private putUrl  = ApiGateway.identity.role.base + ApiGateway.identity.role.putRole;
  private setPublicUrl = ApiGateway.identity.role.base + ApiGateway.identity.role.setPublic;
  private removePublicUrl = ApiGateway.identity.role.base + ApiGateway.identity.role.removePublic;
  private deleteUrl = ApiGateway.identity.role.base + ApiGateway.identity.role.deleteRole;
  constructor(private http: HttpClient) { }
  getRole(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getRoleUrl}`);
  }
  performCreateEditRole( title:string, hasPublic: boolean, id:string, rank: number): Observable<any>{
    if(id){
      return this.http.put<any>(`${this.baseUrl + this.putUrl}`, { name : title, isPublic: hasPublic, id: id, rank: rank})
    }else{
       return this.http.post<any>(`${this.baseUrl + this.postUrl}`, { name : title, isPublic: hasPublic, rank: rank})
    }
  }
  setPublic(id: string, isActivate: boolean): Observable<any>{
    if(isActivate){
      return this.http.get<any>(`${this.baseUrl + this.setPublicUrl}/${id}`);
    }else{
      return this.http.get(`${this.baseUrl + this.removePublicUrl}/${id}`);
    }
  }
  deleteRole(id: string){
    return this.http.delete<any>(`${this.baseUrl + this.deleteUrl + '/'+ id}`)
  }
}
