import { formatDate } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import NepaliDate from "nepali-date-converter";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { PackageModel } from "src/app/infrastructure/model/UserManagement/resource/package/packagemodel";
import Swal from "sweetalert2";
import { PackageService } from "../../service/package.service";

@Component({
  selector: "app-promo-create",
  templateUrl: "./promo-create.component.html",
  styleUrls: ["./promo-create.component.css"],
})
export class PromoCreateComponent implements OnInit {
  createPromoForm: FormGroup;
  packages: PackageModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private packageService: PackageService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.packages = this.route.snapshot.data.packageDDL;
  }

  FormDesign() {
    return (this.createPromoForm = this.form.group({
      packageId: [null, Validators.required],
      promoCode: [null, Validators.required],
      startDateNp: [null, Validators.required],
      endDateNp: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      hasDiscountPercent: [false, Validators.required],
      discount: [null]
    }));
  }

  createPromo() {
    Swal.fire({
      title: "Create Promo Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        debugger;
        this.packageService
          .createPromo(this.createPromoForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              Swal.fire("Created!", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }

  changeNepaliToEnglishStartDate(val: { formattedDate: string }) {
    var date = this.dateConverter(val);
    this.createPromoForm.patchValue({
      startDate: date
    });
  }

  changeNepaliToEnglishEndDate(val: { formattedDate: string }) {
    var date = this.dateConverter(val);
    this.createPromoForm.patchValue({
      endDate: date
    });
  }

  dateConverter(val: { formattedDate: string }) {
    var dateValue = val.formattedDate;
    if (dateValue) {
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1];
      var d: number = +date[0];
      var actualDate = new NepaliDate(y, m, d);
      var npDate = actualDate.format("DD/MM/YYYY", "np").toString();
      val.formattedDate = npDate;
      require("nepali-date-converter");
      return formatDate(actualDate.toJsDate(), "dd/MM/yyyy", "en-US");
    }
  }
}
