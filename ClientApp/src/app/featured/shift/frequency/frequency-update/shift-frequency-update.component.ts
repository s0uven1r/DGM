import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { ShiftService } from '../../service/shift.service';

@Component({
  selector: 'app-shift-frequency-update',
  templateUrl: './shift-frequency-update.component.html',
  styleUrls: ['./shift-frequency-update.component.css']
})

export class ShiftFrequencyUpdateComponent implements OnInit {

  updateShiftFrequencyForm: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private ShiftService: ShiftService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  FormDesign() {
    return (this.updateShiftFrequencyForm = this.form.group({
      id:[null, Validators.required],
      Name: [null, Validators.required],
      IsActive: [false, Validators.required],
      Duration: [0, Validators.required]
    }));
  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.ShiftService.getSingleShiftFrequency(params["id"]).subscribe((x) => {
          this.updateShiftFrequencyForm.patchValue({
            id: x.id,
            Name: x.name,
            IsActive: x.isActive,
            Duration: x.duration
          });
        });
      }
    });
  }

  updateShiftFrequency() {
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
          .UpdateShiftFrequency(this.updateShiftFrequencyForm.value)
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
