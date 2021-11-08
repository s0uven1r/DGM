import { formatDate } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import NepaliDate from 'nepali-date-converter';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PackageModel } from "src/app/infrastructure/model/UserManagement/resource/package/packagemodel";
import { ShiftModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';
import Swal from 'sweetalert2';
import { PackageService } from '../service/package.service';

@Component({
  selector: 'app-customer-package',
  templateUrl: './customer-package.component.html',
  styleUrls: ['./customer-package.component.css']
})
export class CustomerPackageComponent implements OnInit, AfterViewInit {
  registerPackageForm: FormGroup;
  packages: PackageModel[] = [];
  shifts: ShiftModel[] = [];
  errorMsg: string = '';
  constructor(
    private packageService: PackageService,
    private route: ActivatedRoute,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.packages = this.route.snapshot.data.packageDDL;
    this.shifts = this.route.snapshot.data.shifts;
  }
  ngAfterViewInit(){
    var div = (document.getElementsByClassName('datePickerDiv'));
    div[0].children[0].children[0].className = "";
    div[0].children[0].children[0].className = "form-control";
   }
  FormDesign() {
    return (this.registerPackageForm = this.form.group({
      packageId: [null, Validators.required],
      shiftId: [null, Validators.required],
      startDateNP: [null, Validators.required],
      startDateEN: [null, Validators.required],
      endDateNP: [null, Validators.required],
      endDateEN: [null, Validators.required],
      email: [null,Validators.required],
      confirmEmail: [null],
      firstName: [null,Validators.required],
      middleName: [null],
      lastName: [null,Validators.required],
      paidAmount: [0],
      promoCode: [null],
      packageTotalDays: [0],
      packageAmount: [0],
      discountAmount: [0]
    }));
  }
  changeNepaliToEnglish(val: { formattedDate: string; }){
    var dateValue = val.formattedDate;
    if(dateValue){
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1] ;
      var d: number = +date[0];
      var actualDate = new NepaliDate(y,m,d) ;
      var totalDays = this.registerPackageForm.controls['packageTotalDays'].value;
      var endDate = new NepaliDate(y,m,(d + totalDays-1)) ;
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      var npEndDate = endDate.format('DD/MM/YYYY', 'np').toString();
      val.formattedDate = npDate;
      require('nepali-date-converter');
      var dateAd = formatDate(actualDate.toJsDate(),"dd/MM/yyyy","en-US");
      var endDateAd = formatDate(endDate.toJsDate(),"dd/MM/yyyy","en-US");
      this.registerPackageForm.patchValue({
        startDateEN: dateAd,
        endDateNP: npEndDate,
        endDateEN: endDateAd
      });
        
    }
  }
  setPackageDetails(packageVal: { value: any; }){
    var packageDetails = this.route.snapshot.data.packageDDL.filter( (arr: { [x: string]: any; }) => arr['id']===packageVal.value)[0]
    this.registerPackageForm.patchValue({
      packageTotalDays: packageDetails['totalDay'],
      packageAmount: packageDetails['price']
    });
  }
  registerCustomerPackage(){
    Swal.fire({
      title: "Register Customer Package",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.packageService
          .registerCustomerPackage(this.registerPackageForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              Swal.fire("Register Package!", "User Action", "success");
            },
            ()=> {
              console.log("HTTP request completed.")
            }
          );
      }
    });
  }

  validatePaidAmount(){
    this.errorMsg = '';
    var paidAmount = this.registerPackageForm.controls['paidAmount'].value;
    var packageAmount = this.registerPackageForm.controls['packageAmount'].value;
    var discountAmount = this.registerPackageForm.controls['discountAmount'].value;
    var netAmount = packageAmount - discountAmount;
    if(paidAmount > netAmount){
      this.errorMsg = `Paid Amount Rs.${paidAmount} is greater than package net amount Rs.${netAmount}`;
    }
  }
  checkPromoCode(){
    this.packageService
    .getSinglePromoByPromocode(this.registerPackageForm.controls['promoCode'].value)
    .subscribe(
      (data) => {
        var packageAmount = this.registerPackageForm.controls['packageAmount'].value;
        var discountAmount = 0;
        if(data['hasDiscountPercent']){
            discountAmount = (packageAmount* data['discount'])/100;
        }else{
          discountAmount = data['discount'];
        }
        this.registerPackageForm.patchValue({
          discountAmount: discountAmount
        });
      },
      ()=> {
        console.log("HTTP request completed.")
      }
    );
  }
}


