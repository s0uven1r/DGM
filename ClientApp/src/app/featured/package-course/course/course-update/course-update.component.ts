import { CourseModel } from 'src/app/infrastructure/model/UserManagement/resource/course/coursemodel';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { CourseService } from '../../service/Course.service';

@Component({
  selector: 'app-Course-Update',
  templateUrl: './Course-Update.component.html',
  styleUrls: ['./Course-Update.component.css']
})
export class CourseUpdateComponent implements OnInit {
  UpdateCourseForm: FormGroup;
  courses: CourseModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private CourseService: CourseService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.courses = this.route.snapshot.data.courseDDL;
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.CourseService.getSingleCourse(params["id"]).subscribe((x) => {
          this.UpdateCourseForm.patchValue({
            id: x.id,
            CourseName: x.courseName,
            courseTypeId: x.courseTypeId,
            courseInfo: x.courseInfo,
            requiredDocuments: x.requiredDocuments,
            isAdvanceCourse: x.isAdvanceCourse,
          });
        });
      }
    });
  }

  FormDesign() {
    return (this.UpdateCourseForm = this.form.group({
      id: [null, Validators.required],
      CourseName: [null, Validators.required],
      courseTypeId: [null, Validators.required],
      courseInfo: [null, Validators.required],
      requiredDocuments: [null, Validators.required],
      isAdvanceCourse: [false, Validators.required],
    }));
  }

  UpdateCourse() {
    Swal.fire({
      title: "Update Course Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.CourseService
          .UpdateCourse(this.UpdateCourseForm.value)
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