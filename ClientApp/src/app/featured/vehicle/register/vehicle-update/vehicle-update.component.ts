import { formatDate } from '@angular/common';
import { AfterViewInit, ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import NepaliDate from 'nepali-date-converter';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { VehicleService } from '../../service/vehicle.service';

@Component({
  selector: 'app-vehicle-update',
  templateUrl: './vehicle-update.component.html',
  styleUrls: ['./vehicle-update.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class VehicleUpdateComponent implements OnInit, AfterViewInit {
  @ViewChild('dateVal', { static: true }) dateVal: ElementRef;
  updateInventoryForm: FormGroup;
  constructor(
    private route: ActivatedRoute,
    private vehicleService: VehicleService,
    private form: FormBuilder,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
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
              manufacturer: x.manufacturer,
              registerDateEN: x.registerDateEN
            });
          if( x.registerDateNP){
            var date =  x.registerDateNP.split("/", 3);
            var y: number = +date[2];
            var m: number = +date[1] ;
            var d: number = +date[0];
            var actualDate = new NepaliDate(y,m,d) ;
            var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
           this.dateVal['formattedDate'] = npDate;
          }
          this.changeDetectorRef.markForCheck();
          });
      }
    });
  }
  ngAfterViewInit(){
    var div = (document.getElementsByClassName('datePickerDiv'));
    div[0].children[0].children[0].className = "";
    div[0].children[0].children[0].className = "form-control";
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
      manufacturer: [null],
      registerDateNP: [null],
      registerDateEN: [null]
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
              Swal.fire('Updated!', 'User Action', 'success');
            },
            () => console.log('HTTP request completed.')
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
        require('nepali-date-converter');
        var dateAd = formatDate(actualDate.toJsDate(),"dd/MM/yyyy","en-US");
        this.updateInventoryForm.patchValue({
          registerDateEN: dateAd
        });

    }
  }
}
