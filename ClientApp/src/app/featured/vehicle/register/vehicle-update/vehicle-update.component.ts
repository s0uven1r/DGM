import { Component, OnInit } from "@angular/core";
import {  FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { VehicleInventoryModel } from "src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model";
import Swal from "sweetalert2";
import { VehicleService } from "../../service/vehicle.service";

@Component({
  selector: "app-vehicle-update",
  templateUrl: "./vehicle-update.component.html",
  styleUrls: ["./vehicle-update.component.css"],
})
export class VehicleUpdateComponent implements OnInit {
  updateInventoryForm: FormGroup;
  vehicleData: VehicleInventoryModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private form: FormBuilder
  ) {
    this.UpdateInventoryFormDesign();
  }

  ngOnInit(): void {
    this.vehicleData = this.route.snapshot.data.vehicleData;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.vehicleService
          .getVehicleDetailById(params["id"])
          .subscribe((x) => {
            this.updateInventoryForm.patchValue({
              id: params["id"],
              registrationNumber: x.registrationNumber,
              engineNumber: x.engineNumber,
              chasisNumber: x.chasisNumber,
              model: x.model,
              subModel: x.subModel,
              capacity: x.capacity,
              manufacturedYear: x.manufacturedYear,
              price: x.price,
            });
          });
      }
    });
  }

  UpdateInventoryFormDesign() {
    return (this.updateInventoryForm = this.form.group({
      id: [null],
      registrationNumber: [null],
      engineNumber: [null],
      chasisNumber: [null],
      model: [null],
      subModel: [null],
      capacity: [null],
      manufacturedYear: [null],
      price: [null],
    }));
  }

  updateVehicleInventory() {
    Swal.fire({
      title: "Update Vehicle Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
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
              Swal.fire("Updated!", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }
}
