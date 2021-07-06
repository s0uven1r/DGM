import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { VehicleService } from '../../service/vehicle.service';

@Component({
  selector: 'app-vehicle-create',
  templateUrl: './vehicle-create.component.html',
  styleUrls: ['./vehicle-create.component.css']
})
export class VehicleCreateComponent implements OnInit {
  createInventoryForm: FormGroup;
  constructor(private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private form: FormBuilder) { 
      this.CreateInventoryFormDesign();
    }

  ngOnInit(): void {
    
  }

  CreateInventoryFormDesign() {
    return this.createInventoryForm = this.form.group({
      registrationNumber: [null],
      engineNumber: [null],
      chasisNumber: [null],
      model: [null],
      subModel: [null],
      capacity: [null],
      manufacturedYear: [null],
      price: [null],
    });
  }

  createVehicleInventory(){
    Swal.fire({
      title: 'Add Vehicle Detail',
      text: 'User Action',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Ok',
      cancelButtonText: 'No'
      }).then((result) => {
          if (result.value) {
        this.vehicleService.createVehicleInventory(this.createInventoryForm.value)
          .pipe(catchError(err => {
            return throwError(err);
        }))
          .subscribe(() => {
                    this.createInventoryForm.reset(); this.createInventoryForm.clearValidators();
                    Swal.fire(
                      'Added!',
                      'User Action',
                      'success'
                  )},
          () => console.log('HTTP request completed.') );
      }
    })};
}
