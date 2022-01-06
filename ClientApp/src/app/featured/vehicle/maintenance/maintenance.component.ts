import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { VehicleControllersClaim } from 'src/app/infrastructure/datum/claim/vehicle-management';
import { VehicleMaintenanceModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-maintenance-model';
import Swal from 'sweetalert2';
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
  isDtInitialized:boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  constructor(private vehicleService: VehicleService,private router: Router,
     private changeDetectorRef: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]]
    };
   this.getInitData();
  }
  

  updateVehicleDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/vehicle/maintain/edit/${id}`])
    );
    window.open(url, "_self");
  }

  deleteVehicleDetail(id: string) {
    Swal.fire({
      title: "Delete maintenance vehicle details?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.vehicleService.deleteVehicleMaintenanceDetailById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Vehicle maintenance detail deleted successfully.",
              "success"
            );
            this.getInitData();
          },
          (err) => {
            Swal.fire("Error Deleted!", err, "error");
          }
        );
      }
    });
  }
  getInitData(){
    this.vehicleService.getMaintenance().subscribe(x => {this.vehicleData = x;
      if (this.isDtInitialized) {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next();
        });
      } else {
        this.isDtInitialized = true
        this.dtTrigger.next();
      }
      this.changeDetectorRef.markForCheck();
    });
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
}
