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
  private getInventoryUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.getAll;
  private getInventoryByIdUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.getSingleById;
  private createInventoryUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.create;
  private updateInventoryUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.update;
  private deleteInventoryUrl = ApiGateway.resource.vehicle.inventory.base + ApiGateway.resource.vehicle.inventory.delete;
  constructor(private http: HttpClient) { }

  getMaintenance(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getMaintenanceUrl}`);
  }
  getInventory(): Observable<any>{
    return this.http.get<any>(`${this.baseUrl + this.getInventoryUrl}`);
  }
  getVehicleDetailById(id: string): Observable<any>{
    debugger;
    return this.http.get<any>(`${this.baseUrl + this.getInventoryByIdUrl}/${id}`);
  }
  createVehicleInventory(value: any){
    return this.http
    .post<any>(`${this.baseUrl + this.createInventoryUrl}`, {
      registrationNumber: value.registrationNumber,
      chasisNumber :value.chasisNumber,
      engineNumber :value.engineNumber,
      model :value.model,
      subModel :value.subModel,
      capacity :value.capacity,
      manufacturedYear :value.manufacturedYear
    });
  }
  updateVehicleInventory(value: any){
    return this.http
    .put<any>(`${this.baseUrl + this.updateInventoryUrl}/${value.id}`, {
      registrationNumber: value.registrationNumber,
      chasisNumber :value.chasisNumber,
      engineNumber :value.engineNumber,
      model :value.model,
      subModel :value.subModel,
      capacity :value.capacity,
      manufacturedYear :value.manufacturedYear
    });
  }
  deleteVehicleDetailById(id: string): Observable<any>{
    return this.http.delete<any>(`${this.baseUrl + this.deleteInventoryUrl}/${id}`);
  }
}