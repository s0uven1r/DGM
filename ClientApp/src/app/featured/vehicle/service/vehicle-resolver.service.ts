import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { VehicleInventoryModel } from "src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model";
import { VehicleService } from "./vehicle.service";

@Injectable({
    providedIn: 'root'
  })
  export class VehicleResolverService implements Resolve <Observable<VehicleInventoryModel[]>>{
  
    constructor( private vehicleService: VehicleService ) {
      }
      resolve(_route: ActivatedRouteSnapshot, _state: RouterStateSnapshot){
        return this.vehicleService.getInventory();
      }
  
  }