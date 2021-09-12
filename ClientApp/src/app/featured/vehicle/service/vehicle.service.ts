import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ApiGateway } from "src/app/infrastructure/datum/apigateway/api-gateway";
import { environment } from "src/environments/environment";

@Injectable({
  providedIn: "root",
})
export class VehicleService {
  private baseUrl = environment.resourceUrl;
  private getMaintenanceUrl = ApiGateway.resource.vehicle.maintenance.base;
  private getInventoryUrl =
    ApiGateway.resource.vehicle.inventory.base +
    ApiGateway.resource.vehicle.inventory.getAll;
  private getInventoryByIdUrl =
    ApiGateway.resource.vehicle.inventory.base +
    ApiGateway.resource.vehicle.inventory.getSingleById;
  private createInventoryUrl =
    ApiGateway.resource.vehicle.inventory.base +
    ApiGateway.resource.vehicle.inventory.create;
  private updateInventoryUrl =
    ApiGateway.resource.vehicle.inventory.base +
    ApiGateway.resource.vehicle.inventory.update;
  private getAccountDetailsUrl = ApiGateway.resource.vehicle.inventory.base +
  ApiGateway.resource.vehicle.inventory.getAccountNumberDetails;
  private deleteInventoryUrl =
    ApiGateway.resource.vehicle.inventory.base +
    ApiGateway.resource.vehicle.inventory.delete;

  private createMaintenanceUrl =
    ApiGateway.resource.vehicle.maintenance.base +
    ApiGateway.resource.vehicle.maintenance.create;

  constructor(private http: HttpClient) {}

  getMaintenance(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getMaintenanceUrl}`);
  }
  getSingeMaintenance(id: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getMaintenanceUrl}/${id}`);
  }
  getInventory(): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getInventoryUrl}`);
  }
  getVehicleDetailById(id: string): Observable<any> {
    return this.http.get<any>(
      `${this.baseUrl + this.getInventoryByIdUrl}/${id}`
    );
  }
  createVehicleInventory(value: any) {
    return this.http.post<any>(`${this.baseUrl + this.createInventoryUrl}`, {
      registrationNumber: value.registrationNumber,
      chasisNumber: value.chasisNumber,
      engineNumber: value.engineNumber,
      model: value.model,
      subModel: value.subModel,
      capacity: value.capacity,
      manufacturedYear: value.manufacturedYear,
      manufacturer: value.manufacturer,
      registerDateNP:
        value.registerDateNP.day +
        "/" +
        value.registerDateNP.month +
        "/" +
        value.registerDateNP.year,
      registerDateEN: value.registerDateEN,
    });
  }
  updateVehicleInventory(value: any) {
    return this.http.put<any>(
      `${this.baseUrl + this.updateInventoryUrl}/${value.id}`,
      {
        registrationNumber: value.registrationNumber,
        chasisNumber: value.chasisNumber,
        engineNumber: value.engineNumber,
        model: value.model,
        subModel: value.subModel,
        capacity: value.capacity,
        manufacturedYear: value.manufacturedYear,
        manufacturer: value.manufacturer,
        registerDateNP:
          value.registerDateNP.day +
          "/" +
          value.registerDateNP.month +
          "/" +
          value.registerDateNP.year,
        registerDateEN: value.registerDateEN,
      }
    );
  }
  deleteVehicleDetailById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.deleteInventoryUrl}/${id}`
    );
  }

  performAddUpdateVehicleMaintenance(value: any) {
    if (value.id) {
      return this.http.put<any>(
        `${this.baseUrl + this.getMaintenanceUrl}/${value.id}`,
        {
          VehicleId: value.vehicleId,
          Remark: value.remarks,
          RegisterDateNP:
            value.registerDateNP.day +
            "/" +
            value.registerDateNP.month +
            "/" +
            value.registerDateNP.year,
          RegisterDateEN: value.registerDateEN,
        }
      );
    }
    return this.http.post<any>(`${this.baseUrl + this.createMaintenanceUrl}`, {
      VehicleId: value.vehicleId,
      Remark: value.remarks,
      registerDateNP:
        value.registerDateNP.day +
        "/" +
        value.registerDateNP.month +
        "/" +
        value.registerDateNP.year,
      registerDateEN: value.registerDateEN,
    });
  }
  deleteVehicleMaintenanceDetailById(id: string): Observable<any> {
    return this.http.delete<any>(
      `${this.baseUrl + this.getMaintenanceUrl}/${id}`
    );
  }
  getAllAccountDetails(name: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl + this.getAccountDetailsUrl}?value=${name}`);
  }
}
