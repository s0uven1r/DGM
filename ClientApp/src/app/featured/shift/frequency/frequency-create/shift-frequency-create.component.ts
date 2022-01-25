import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { ShiftService } from '../../service/shift.service';

@Component({
  selector: 'app-shift-frequency-create',
  templateUrl: './shift-frequency-create.component.html',
  styleUrls: ['./shift-frequency-create.component.css']
})
export class ShiftFrequencyCreateComponent implements OnInit {
  createShiftFrequencyForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private ShiftService: ShiftService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
  }

  FormDesign() {
    return (this.createShiftFrequencyForm = this.form.group({
      Name: [null, Validators.required],
      IsActive: [false, Validators.required],
      Duration: [0, Validators.required]
    }));
  }

  createShiftFrequency(){
    Swal.fire({
      title: "Create Shift Frequency",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.ShiftService
          .createShiftFrequency(this.createShiftFrequencyForm.value)
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
