import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ShiftFrequencyModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';
import Swal from 'sweetalert2';
import { ShiftService } from '../../service/shift.service';

@Component({
  selector: 'app-shift-update',
  templateUrl: './shift-update.component.html',
  styleUrls: ['./shift-update.component.css']
})
export class ShiftUpdateComponent implements OnInit {
  
  updateShiftForm: FormGroup;
  shiftFrequencies: ShiftFrequencyModel[] = [];
  
  constructor(
    private route: ActivatedRoute,
    private ShiftService: ShiftService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.shiftFrequencies = this.route.snapshot.data.shiftFrequenciesDDL;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.ShiftService.getSingleShift(params["id"]).subscribe((x) => {
          this.updateShiftForm.patchValue({
            id: x.id,
            Name: x.name,
            IsActive: x.isActive,
            ShiftFrequencyId: x.shiftFrequencyId,
            StartTime: x.startTime
          });
        });
      }
    });
  }

  FormDesign() {
    return (this.updateShiftForm = this.form.group({
      id: [null, Validators.required],
      Name: [null, Validators.required],
      IsActive: [false, Validators.required],
      ShiftFrequencyId: [null, Validators.required],
      StartTime: [null, Validators.required]
    }));
  }

  updateShift() {
    Swal.fire({
      title: "Update Course Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.ShiftService
          .UpdateShift(this.updateShiftForm.value)
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

}
