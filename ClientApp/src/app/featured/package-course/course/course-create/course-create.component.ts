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
  templateUrl: './course-create.component.html',
  styleUrls: ['./course-create.component.css']
})
export class CourseCreateComponent implements OnInit {
  createCourseForm: FormGroup;
  courses: CourseModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private courseService: CourseService,
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
      courseTypeId: [null, Validators.required],
      courseInfo: [null, Validators.required],
      requiredDocuments: [null, Validators.required],
      isAdvanceCourse: [false, Validators.required],
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
        this.courseService
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
