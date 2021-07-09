import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { VehicleService } from '../../service/vehicle.service';

@Component({
  selector: 'app-vehicle-update',
  templateUrl: './vehicle-update.component.html',
  styleUrls: ['./vehicle-update.component.css'],
})
export class VehicleUpdateComponent implements OnInit {
  updateInventoryForm: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      console.log(params);
      console.log(this.route.snapshot.data);
      if (params['id']) {
        this.vehicleService
          .getVehicleDetailById(params['id'])
          .subscribe((x) => {
            this.updateInventoryForm.patchValue({
              id: x.id,
              registrationNumber: x.registrationNumber,
              engineNumber: x.engineNumber,
              chasisNumber: x.chasisNumber,
              model: x.model,
              subModel: x.subModel,
              capacity: x.capacity,
              manufacturedYear: x.manufacturedYear,
            });
          });
      }
    });
  }

  FormDesign() {
    return (this.updateInventoryForm = this.form.group({
      id: [null],
      registrationNumber: [null, Validators.required],
      engineNumber: [null, Validators.required],
      chasisNumber: [null, Validators.required],
      model: [null],
      subModel: [null],
      capacity: [null],
      manufacturedYear: [null],
    }));
  }

  updateVehicleInventory() {
    Swal.fire({
      title: 'Update Vehicle Detail',
      text: 'User Action',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ok',
      cancelButtonText: 'No',
    }).then((result) => {
      if (result.value) {
        this.vehicleService
          .updateVehicleInventory(this.updateInventoryForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              this.updateInventoryForm.reset();
              this.updateInventoryForm.clearValidators();
              Swal.fire('Updated!', 'User Action', 'success');
            },
            () => console.log('HTTP request completed.')
          );
      }
    });
  }
}
