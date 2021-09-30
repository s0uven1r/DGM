import { CourseModel } from 'src/app/infrastructure/model/UserManagement/resource/course/coursemodel';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { CourseService } from '../../service/Course.service';

@Component({
  selector: 'app-Course-create',
  templateUrl: './Course-create.component.html',
  styleUrls: ['./Course-create.component.css']
})
export class CourseCreateComponent implements OnInit {
  createCourseForm: FormGroup;
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
  }

  FormDesign() {
    return (this.createCourseForm = this.form.group({
      CourseName: [null, Validators.required],
      courseId: [null, Validators.required],
      totalDay: [0, Validators.required],
      duration: [0, Validators.required],
      price: [0, Validators.required],
    }));
  }

  createCourse() {
    Swal.fire({
      title: "Create Course Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.CourseService
          .createCourse(this.createCourseForm.value)
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