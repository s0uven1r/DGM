import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { CourseTypeModel } from 'src/app/infrastructure/model/UserManagement/resource/course/coursetypemodel';
import Swal from 'sweetalert2';
import { CourseTypeService } from '../../service/course-type.service';

@Component({
  selector: 'app-course-type-create',
  templateUrl: './course-type-create.component.html',
  styleUrls: ['./course-type-create.component.css']
})
export class CourseTypeCreateComponent implements OnInit {
  createCourseTypeForm: FormGroup;
  courses: CourseTypeModel[] = [];
  
  constructor(private route: ActivatedRoute,
  private CourseTypeService: CourseTypeService,
  private form: FormBuilder
) {
  this.FormDesign();
}

  ngOnInit(): void {
  }

  FormDesign() {
    return (this.createCourseTypeForm = this.form.group({
      Title: [null, Validators.required]
    }));
  }

  createCourseType() {
    Swal.fire({
      title: "Create Course Type",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.CourseTypeService
          .createCourseType(this.createCourseTypeForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              Swal.fire("Created!", "User Action", "success");
            },
            ()=> {
              console.log("HTTP request completed.")
            }
          );
      }
    });
  }

}
