import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { CourseModel } from "src/app/infrastructure/model/UserManagement/resource/course/coursemodel";
import { ShiftFrequencyModel } from "src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model";
import Swal from "sweetalert2";
import { PackageService } from "../../service/package.service";

@Component({
  selector: "app-package-create",
  templateUrl: "./package-create.component.html",
  styleUrls: ["./package-create.component.css"],
})
export class PackageCreateComponent implements OnInit {
  createPackageForm: FormGroup;
  courses: CourseModel[] = [];
  shiftFrequency: ShiftFrequencyModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private packageService: PackageService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.courses = this.route.snapshot.data.courseDDL;
    this.shiftFrequency = this.route.snapshot.data.shiftFrequency;
  }

  FormDesign() {
    return (this.createPackageForm = this.form.group({
      packageName: [null, Validators.required],
      courseId: [null, Validators.required],
      totalDay: [0, Validators.required],
      shiftFrequencyId: [null, Validators.required],
      price: [0, Validators.required],
      description: [null, Validators.required],
    }));
  }

  createPackage() {
    Swal.fire({
      title: "Create Package Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.packageService
          .createPackage(this.createPackageForm.value)
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
