import { formatDate } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import NepaliDate from 'nepali-date-converter';
import { PackageModel } from "src/app/infrastructure/model/UserManagement/resource/package/packagemodel";
import { ShiftModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';

@Component({
  selector: 'app-customer-package',
  templateUrl: './customer-package.component.html',
  styleUrls: ['./customer-package.component.css']
})
export class CustomerPackageComponent implements OnInit, AfterViewInit {
  registerPackageForm: FormGroup;
  packages: PackageModel[] = [];
  shifts: ShiftModel[] = [];
  constructor(
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
    div[1].children[0].children[0].className = "";
    div[1].children[0].children[0].className = "form-control";
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
      paidAmount: [null],
      promoCode: [null]
    }));
  }
  changeNepaliToEnglish(val: { formattedDate: string; }, isEnd: boolean = false){
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
        if(isEnd === true){
          this.registerPackageForm.patchValue({
            endDateEN: dateAd
          });
        }else{
          this.registerPackageForm.patchValue({
            startDateEN: dateAd
          });
        }
    }
  }
}
