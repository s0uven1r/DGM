import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { VehicleControllersClaim } from 'src/app/infrastructure/datum/claim/vehicle-management';
import { VehicleInventoryModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model';
import { VehicleService } from '../service/vehicle.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VehicleRegisterComponent implements OnInit, OnDestroy {

  dtOptions: DataTables.Settings = {};
  vehicles: VehicleInventoryModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  vehicleCreateClaim = [VehicleControllersClaim.Inventory.Write];
  constructor(private vehicleService: VehicleService, private router: Router, private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]]
    };
    this.vehicleService.getInventory().subscribe(x => {this.vehicles = x;
      this.changeDetectorRef.markForCheck();
      this.dtTrigger.next();
    });
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  updateVehicleDetail(id: string){
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/vehicle/update/${id}`])
    );
    window.open(url, "_blank");
  }
}
