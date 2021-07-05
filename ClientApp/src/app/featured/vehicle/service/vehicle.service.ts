import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiGateway } from 'src/app/infrastructure/datum/apigateway/api-gateway';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private baseUrl = environment.resourceUrl;
  private getMaintenanceUrl = ApiGateway.resource.vehicle.maintenance.base;
  private getInventoryUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.GetAll;
  constructor(private http: HttpClient) { }

  getMaintenance(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getMaintenanceUrl}`);
  }
  getInventory(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getInventoryUrl}`);
  }
}