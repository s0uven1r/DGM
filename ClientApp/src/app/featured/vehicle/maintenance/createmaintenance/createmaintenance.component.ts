import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { VehicleInventoryModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model';
import Swal from 'sweetalert2';
import { VehicleService } from '../../service/vehicle.service';

@Component({
  selector: 'app-createmaintenance',
  templateUrl: './createmaintenance.component.html',
  styleUrls: ['./createmaintenance.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreatemaintenanceComponent implements OnInit, OnDestroy {
  vehicles: VehicleInventoryModel[] = [];
  MaintenanceForm: FormGroup;
  private isEdit: boolean;
  constructor(private vehicleService: VehicleService,
    private changeDetectorRef: ChangeDetectorRef, private route: ActivatedRoute,
    private form: FormBuilder) { 
      this.FormDesign();

    }

  ngOnInit(): void {
    // Do not forget to unsubscribe the event
    this.vehicles = this.route.snapshot.data.vehicleData;
    this.route.params.subscribe(params => {
      if(params['id']){
        this.isEdit = true;
        this.vehicleService.getSingeMaintenance(params['id']).subscribe(x => {
          this.MaintenanceForm.patchValue({
            'id': params['id'],
            'vehicleId': x.vehicleId,
            'maintenanceType': x.Type == null? "" : x.Type,
            'remarks': x.remark
        }); 
        })
      }
     
   });
  }
  ngOnDestroy(): void {
  }

  FormDesign() {
    return this.MaintenanceForm = this.form.group({
      id: [null],
      vehicleId: [""],
      maintenanceType: [""],
      remarks: [null],
    });
  }
  registerMaintenance(){
    Swal.fire({
      title: this.isEdit?'Update Vehicle Maintenance Detail': "Add Vehicle Maintenance Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.vehicleService
          .performAddUpdateVehicleMaintenance(this.MaintenanceForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              if( !this.isEdit){
              this.MaintenanceForm.reset();
              this.MaintenanceForm.clearValidators();
              this.MaintenanceForm.patchValue({
                'vehicleId': "",
                'maintenanceType':  "" ,
              }); 
            }

            Swal.fire(
              this.isEdit?'Updated':'Added!',
              'User Action',
              'success'
            );
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }
  
}
