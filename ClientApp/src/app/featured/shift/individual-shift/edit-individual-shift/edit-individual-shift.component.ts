import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';
import { PackageModel } from "src/app/infrastructure/model/UserManagement/resource/package/packagemodel";
import { ShiftModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';
import NepaliDate from 'nepali-date-converter';
import { formatDate } from '@angular/common';
import { VehicleInventoryModel } from 'src/app/infrastructure/model/UserManagement/resource/vehicle/vehicle-inventory-model';
import { UserService } from '../../../identity/user/service/user.service';
import { ShiftService } from '../../service/shift.service';
import { Observable, of, throwError } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';

@Component({
  selector: 'app-edit-individual-shift',
  templateUrl: './edit-individual-shift.component.html',
  styleUrls: ['./edit-individual-shift.component.css']
})
export class EditIndividualShiftComponent implements OnInit {
  @ViewChild("dateVal", { static: true }) dateVal: ElementRef;
  UpdateIndividualShiftForm: FormGroup;
  packages: PackageModel[] = [];
  shifts: ShiftModel[] = [];
  vehicles: VehicleInventoryModel[] = [];
  result = [];
  resultPair = [];
  constructor( private route: ActivatedRoute,
    private form: FormBuilder,private userService: UserService,
    private shiftService: ShiftService,
    private changeDetectorRef: ChangeDetectorRef) {
        this.FormDesign();
     }

  ngOnInit(): void {
    this.packages = this.route.snapshot.data.packageDDL;
    this.shifts = this.route.snapshot.data.shifts;
    this.vehicles = this.route.snapshot.data.vehicleData;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.shiftService.getSingleIndividualShift(params["id"]).subscribe((x) => {
          this.UpdateIndividualShiftForm.patchValue({
            id: x.id,
            packageName: x.packageName,
            shiftId: x.shiftId,
            trainingDateEN: x.trainingDate,
            vehicleId: x.vehicleId,
            accountNumber: x.trainerId,
            trainerName: x.trainerDetail,
          });
          if (x.trainingDateNp) {
            var date = x.trainingDateNp.split("/", 3);
            var y: number = +Number(date[2]);
            var m: number = +date[1];
            var d: number = +date[0];
            var actualStartDate = new NepaliDate(y, m, d);
            var npStartDate = actualStartDate.format("DD/MM/YYYY", "np").toString();
            require("nepali-date-converter");
            this.dateVal["formattedDate"] = npStartDate;
          }
        });
      }
    });
  }
  FormDesign() {
    return (this.UpdateIndividualShiftForm = this.form.group({
      id: [null, Validators.required],
      shiftId: [null, Validators.required],
      packageName: [null, Validators.required],
      trainingDateNP: [null, Validators.required],
      trainingDateEN: [null, Validators.required],
      vehicleId: [null, Validators.required],
      trainerName: [null, Validators.required],
      accountNumber: [null, Validators.required]
    }));
  }
  UpdateIndividualShift() {
    Swal.fire({
      title: "Update Individual Shift",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.shiftService
          .UpdateIndividualShift(this.UpdateIndividualShiftForm.value)
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
      this.UpdateIndividualShiftForm.patchValue({
        trainingDateEN: dateAd,
      });
        
    }
  }
  searchItem(index?: number) {
    return (text$: Observable<string>) => {
      return text$.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(term => this.searchGetCall(term, index).pipe(
          catchError(() => {
            return of([]);
          })))
      )
    }
}
search = (text$: Observable<string>) => {
  return text$.pipe(
    debounceTime(300),
    distinctUntilChanged(),
    switchMap(term => this.searchGetCall(term).pipe(
      catchError(() => {
        return of([]);
      })))
  )
}

searchGetCall(term: string, _index?: number) {
  if (term === '') {
    return of([]);
  }
    return this.userService.getAllAccountTrainerDetails(term).pipe(map(x => {
      this.result = [];
       x.forEach((element: { key: any; }) => {
         this.result.push(element.key);
       });
       this.resultPair.push(x);
       this.changeDetectorRef.markForCheck();
       return this.result;
    }));
  }

  formatter = (result: string) => result.toUpperCase();
  selectedItem(item: { item: string; }){
      var accNo = item.item.split('-')[1].trim();
      this.UpdateIndividualShiftForm.controls['accountNumber'].setValue(accNo);
    }
}
