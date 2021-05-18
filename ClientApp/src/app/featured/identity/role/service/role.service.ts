import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable()
export class RoleService {
  private baseUrl = environment.apiIdentityUrl;
  constructor(private http: HttpClient) { }
  getRole(): Observable<any>{
    return this.http.get(`${this.baseUrl}Roles/GetRoles`);
  }
}
