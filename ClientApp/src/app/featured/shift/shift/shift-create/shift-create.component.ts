import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ShiftFrequencyModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';
import Swal from 'sweetalert2';
import { ShiftService } from '../../service/shift.service';

@Component({
  selector: 'app-shift-create',
  templateUrl: './shift-create.component.html',
  styleUrls: ['./shift-create.component.css']
})
export class ShiftCreateComponent implements OnInit {
  createShiftForm: FormGroup;
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
  }

  FormDesign() {
    return (this.createShiftForm = this.form.group({
      ShiftName: [null, Validators.required],
      ShiftFrequencyId: [null, Validators.required],
      IsActive: [false, Validators.required],
      StartTime: [null, Validators.required]
    }));
  }

  createShift() {
    Swal.fire({
      title: "Create Shift Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.ShiftService
          .createShift(this.createShiftForm.value)
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
}
