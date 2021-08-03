import { Component, ElementRef, OnInit, AfterViewInit, ViewChild } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import Swal from "sweetalert2";
import { VehicleService } from "../../service/vehicle.service";
import getAD from 'nepali-date-converter'
import { DatePipe, formatDate } from "@angular/common";
import NepaliDate from "nepali-date-converter";
@Component({
  selector: "app-vehicle-create",
  templateUrl: "./vehicle-create.component.html",
  styleUrls: ["./vehicle-create.component.css"],
})
export class VehicleCreateComponent implements OnInit, AfterViewInit {
  createInventoryForm: FormGroup;
  constructor(
    private vehicleService: VehicleService,
    private form: FormBuilder) {
    this.FormDesign();
  }

  ngOnInit(): void {

  }
  ngAfterViewInit(){
   var div = (document.getElementsByClassName('datePickerDiv'));
   div[0].children[0].children[0].className = "";
   div[0].children[0].children[0].className = "form-control";
  }
  FormDesign() {
    return (this.createInventoryForm = this.form.group({
      registrationNumber: [null, Validators.required],
      engineNumber: [null, Validators.required],
      chasisNumber: [null, Validators.required],
      model: [null],
      subModel: [null],
      capacity: [null],
      manufacturedYear: [null],
      manufacturer: [null],
      registerDateNP: [null],
      registerDateEN: [null]
    }));
  }

  createVehicleInventory() {
    Swal.fire({
      title: "Add Vehicle Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.vehicleService
          .createVehicleInventory(this.createInventoryForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              this.createInventoryForm.reset();
              this.createInventoryForm.clearValidators();
              Swal.fire("Added!", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }
  changeNepaliToEnglish(val: { formattedDate: string; }){
    var dateValue = val.formattedDate;
    if(dateValue){
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1] ;
      var d: number = +date[0];
      var actualDate = new NepaliDate(y,m,d) ;
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      val.formattedDate = npDate;

        require('nepali-date-converter');
        var dateAd = formatDate(actualDate.toJsDate(),"dd/MM/yyyy","en-US");
        this.createInventoryForm.patchValue({
          registerDateEN: dateAd
        });


    }
  }
}
