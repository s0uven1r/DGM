import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CourseTypeModel } from 'src/app/infrastructure/model/UserManagement/resource/course/coursetypemodel';
import Swal from 'sweetalert2';
import { CourseTypeService } from '../../service/course-type.service';

@Component({
  selector: 'app-course-type-update',
  templateUrl: './course-type-update.component.html',
  styleUrls: ['./course-type-update.component.css']
})

export class CourseTypeUpdateComponent implements OnInit {

  UpdateCourseTypeForm: FormGroup;
  courseTypes: CourseTypeModel[] = [];

  constructor(  private route: ActivatedRoute,
    private CourseTypeService: CourseTypeService,
    private form: FormBuilder) {
        this.FormDesign();
     }

  ngOnInit(): void {
    this.courseTypes = this.route.snapshot.data.courseDDL;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.CourseTypeService.getSingleCourseType(params["id"]).subscribe((x) => {
          this.UpdateCourseTypeForm.patchValue({
            id: x.id,
            Title: x.title,
          });
        });
      }
    });
  }

  FormDesign() {
    return (this.UpdateCourseTypeForm = this.form.group({
      id: [null, Validators.required],
      Title: [null, Validators.required]
    }));
  }

  UpdateCourseType() {
    Swal.fire({
      title: "Update Course Type",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.CourseTypeService
          .UpdateCourseType(this.UpdateCourseTypeForm.value)
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
