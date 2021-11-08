import { formatDate } from "@angular/common";
import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from "@angular/core";
import NepaliDate from "nepali-date-converter";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { PackageModel } from "src/app/infrastructure/model/UserManagement/resource/package/packagemodel";
import Swal from "sweetalert2";
import { PackageService } from "../../service/package.service";

@Component({
  selector: "app-promo-update",
  templateUrl: "./promo-update.component.html",
  styleUrls: ["./promo-update.component.css"],
})
export class PromoUpdateComponent implements OnInit {
  @ViewChild("dateValStart", { static: true }) dateValStart: ElementRef;
  @ViewChild("dateValEnd", { static: true }) dateValEnd: ElementRef;
  updatePromoForm: FormGroup;
  packages: PackageModel[] = [];
  constructor(
    private changeDetectorRef: ChangeDetectorRef,
    private route: ActivatedRoute,
    private packageService: PackageService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.packages = this.route.snapshot.data.packageDDL;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.packageService.getSinglePromo(params["id"]).subscribe((x) => {
          this.updatePromoForm.patchValue({
            id: x.id,
            packageId: x.packageId,
            promoCode: x.promoCode,
            startDate: x.startDate,
            endDate: x.endDate,
            hasDiscountPercent: x.hasDiscountPercent,
            discount: x.discount,
          });
          if (x.startDateNp) {
            var date = x.startDateNp.split("/", 3);
            var y: number = +date[2];
            var m: number = +date[1];
            var d: number = +date[0];
            var actualStartDate = new NepaliDate(y, m, d);
            var npStartDate = actualStartDate.format("DD/MM/YYYY", "np").toString();
            require("nepali-date-converter");
            this.dateValStart["formattedDate"] = npStartDate;
          }
          if (x.endDateNp) {
            var date = x.endDateNp.split("/", 3);
            var y: number = +date[2];
            var m: number = +date[1];
            var d: number = +date[0];
            var actualEndDate = new NepaliDate(y, m, d);
            var npEndDate = actualEndDate.format("DD/MM/YYYY", "np").toString();
            this.dateValEnd["formattedDate"] = npEndDate;
          }
          this.changeDetectorRef.markForCheck();
        });
      }
    });
  }

  // ngAfterViewInit(){
  //   var div = (document.getElementsByClassName('datePickerDiv'));
  //   debugger;
  //   div[0].children[0].children[0].className = "";
  //   div[0].children[0].children[0].className = "form-control";
  //  }

  FormDesign() {
    return (this.updatePromoForm = this.form.group({
      id: [null, Validators.required],
      packageId: [null, Validators.required],
      promoCode: [null],
      startDateNp: [null, Validators.required],
      endDateNp: [null, Validators.required],
      startDate: [null, Validators.required],
      endDate: [null, Validators.required],
      hasDiscountPercent: [false],
      discount: [null],
    }));
  }

  updatePromo() {
    Swal.fire({
      title: "Update Promo Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.packageService
          .updatePromo(this.updatePromoForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              Swal.fire("Updated!", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }

  changeNepaliToEnglishStartDate(val: { formattedDate: string }) {
    debugger;
    var date = this.dateConverter(val);
    this.updatePromoForm.patchValue({
      startDate: date,
    });
  }

  changeNepaliToEnglishEndDate(val: { formattedDate: string }) {
    var date = this.dateConverter(val);
    this.updatePromoForm.patchValue({
      endDate: date,
    });
  }

  dateConverter(val: { formattedDate: string }) {
    debugger;
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
