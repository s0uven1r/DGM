import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
} from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { CourseModel } from "src/app/infrastructure/model/UserManagement/resource/course/coursemodel";
import Swal from "sweetalert2";
import { PackageService } from "../../service/package.service";

@Component({
  selector: "app-package-update",
  templateUrl: "./package-update.component.html",
  styleUrls: ["./package-update.component.css"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PackageUpdateComponent implements OnInit {
  updatePackageForm: FormGroup;
  courses: CourseModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private packageService: PackageService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.courses = this.route.snapshot.data.courseDDL;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.packageService.getSinglePackage(params["id"]).subscribe((x) => {
          this.updatePackageForm.patchValue({
            id: x.id,
            packageName: x.packageName,
            courseId: x.courseId,
            totalDay: x.totalDay,
            duration: x.duration,
            price: x.price
          });
        });
      }
    });
  }

  FormDesign() {
    return (this.updatePackageForm = this.form.group({
      id: [null],
      packageName: [null, Validators.required],
      courseId: [null, Validators.required],
      totalDay: [0, Validators.required],
      duration: [0, Validators.required],
      price: [0, Validators.required],
    }));
  }

  updatePackage() {
    Swal.fire({
      title: "Update Package Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.packageService
          .updatePackage(this.updatePackageForm.value)
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
