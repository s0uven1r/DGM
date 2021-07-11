import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject, Subscription } from 'rxjs';
import { VehicleControllersClaim } from 'src/app/infrastructure/datum/claim/vehicle-management';
import { VehicleInventoryModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model';
import Swal from 'sweetalert2';
import { VehicleService } from '../service/vehicle.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VehicleRegisterComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  vehicles: VehicleInventoryModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  vehicleCreateClaim = [VehicleControllersClaim.Inventory.Write];
  isDtInitialized:boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  constructor(
    private vehicleService: VehicleService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [
        [5, 10, 25, 50, 100, -1],
        [5, 10, 25, 50, 100, "All"],
      ],
    };
   this.getInitData();
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  updateVehicleDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/vehicle/register/edit/${id}`])
    );
    window.open(url, "_self");
    //window.open(url, "_blank");
  }
  deleteVehicleDetail(id: string) {
    Swal.fire({
      title: "Delete vehicle details?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.vehicleService.deleteVehicleDetailById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Vehicle detail deleted successfully.",
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
    this.vehicleService.getInventory().subscribe(x => {this.vehicles = x;
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
}
