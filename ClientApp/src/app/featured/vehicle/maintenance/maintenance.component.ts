import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { VehicleControllersClaim } from 'src/app/infrastructure/datum/claim/vehicle-management';
import { VehicleMaintenanceModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-maintenance-model';
import { VehicleService } from '../service/vehicle.service';

@Component({
  selector: 'app-maintenance',
  templateUrl: './maintenance.component.html',
  styleUrls: ['./maintenance.component.css'], 
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MaintenanceComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  vehicleData: VehicleMaintenanceModel[] = [];
  maintainCreateClaim = [VehicleControllersClaim.Maintenance.Write];
  constructor(private vehicleService: VehicleService,
     private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]]
    };
    this.vehicleService.getMaintenance().subscribe(x => {this.vehicleData = x;
      this.changeDetectorRef.markForCheck();
      this.dtTrigger.next();
    });
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
}
