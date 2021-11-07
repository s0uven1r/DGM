import { formatDate } from '@angular/common';
import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import NepaliDate from 'nepali-date-converter';
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

export class CreatemaintenanceComponent implements OnInit, OnDestroy, AfterViewInit {
  vehicles: VehicleInventoryModel[] = [];
  MaintenanceForm: FormGroup;
  @ViewChild('dateVal', { static: true }) dateVal: ElementRef;
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
      if (params['id']) {
        this.isEdit = true;
        this.vehicleService.getSingeMaintenance(params['id']).subscribe(x => {
          this.MaintenanceForm.patchValue({
            'id': params['id'],
            'vehicleId': x.vehicleId,
            'remarks': x.remark,
            registerDateEN: x.registerDateEN
          });
          if (x.registerDateNP) {
            var date = x.registerDateNP.split("/", 3);
            var y: number = +date[2];
            var m: number = +date[1];
            var d: number = +date[0];
            var actualDate = new NepaliDate(y, m, d);
            var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
            this.dateVal['formattedDate'] = npDate;
          }
          this.changeDetectorRef.markForCheck();

        })
      }

    });
  }

  ngAfterViewInit() {
    var div = (document.getElementsByClassName('datePickerDiv'));
    div[0].children[0].children[0].className = "";
    div[0].children[0].children[0].className = "form-control";
  }
  
  ngOnDestroy(): void {
  }

  FormDesign() {
    return this.MaintenanceForm = this.form.group({
      id: [null],
      vehicleId: [""],
      remarks: [null],
      registerDateNP: [null],
      registerDateEN: [null]
    });
  }

  registerMaintenance() {
    Swal.fire({
      title: this.isEdit ? 'Update Vehicle Maintenance Detail' : "Add Vehicle Maintenance Detail",
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
              if (!this.isEdit) {
                this.MaintenanceForm.reset();
                this.MaintenanceForm.clearValidators();
                this.MaintenanceForm.patchValue({
                  'vehicleId': "",
                  'maintenanceType': "",
                });
              }

              Swal.fire(
                this.isEdit ? 'Updated' : 'Added!',
                'User Action',
                'success'
              );
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }

  changeNepaliToEnglish(val: { formattedDate: string; }) {
    var dateValue = val.formattedDate;
    if (dateValue) {
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1];
      var d: number = +date[0];
      var actualDate = new NepaliDate(y, m, d);
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      val.formattedDate = npDate;

      require('nepali-date-converter');
      var dateAd = formatDate(actualDate.toJsDate(), "dd/MM/yyyy", "en-US");
      this.MaintenanceForm.patchValue({
        registerDateEN: dateAd
      });


    }
  }
}
