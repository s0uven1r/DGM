import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, ChildActivationEnd, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { MenuResultModel } from 'src/app/infrastructure/model/UserManagement/Menu/menu-result-model';
import { environment } from 'src/environments/environment';


@Injectable()
export class NavigationService {
    
    private baseUrl = environment.apiIdentityUrl;
    private getMenuUrl = ApiGateway.identity.menu.base + ApiGateway.identity.menu.getMenu;
   
    constructor(public route: ActivatedRoute, public router: Router, private http: HttpClient) {
        this.router.events
            .pipe(filter(event => event instanceof ChildActivationEnd))
            .subscribe(event => {
                let snapshot = (event as ChildActivationEnd).snapshot;
                while (snapshot.firstChild !== null) {
                    snapshot = snapshot.firstChild;
                }
            });
    }
   
    getMenu(): Observable<MenuResultModel[]> {
        return this.http.get<any>(`${this.baseUrl + this.getMenuUrl}`);;
      }
}
