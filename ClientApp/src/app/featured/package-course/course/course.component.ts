import { CourseService } from './../service/course.service';
import { CourseModel } from './../../../infrastructure/model/UserManagement/resource/course/coursemodel';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import Swal from 'sweetalert2';
import { CourseControllersClaim } from '../../../infrastructure/datum/claim/course-management';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized:boolean = false;
  courseCreateClaim = [CourseControllersClaim.Course.Write];
  courses: CourseModel[] = [];
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  constructor(private courseService: CourseService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [
        [5, 10, 25, 50, 100, -1],
        [5, 10, 25, 50, 100, "All"],
      ],
    };
   this.getInitData();
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  updateCourseDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/config/course/edit/${id}`])
    );
    window.open(url, "_self");
    //window.open(url, "_blank");
  }
  deleteCourseDetail(id: string) {
    Swal.fire({
      title: "Delete Course details?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.courseService.deleteCourseById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Course detail deleted successfully.",
              "success"
            );
           this.getInitData();
          },
          (err) => {
            Swal.fire("Error Deleted!", err, "error");
          }
        );
      }
    });
  }

  getInitData(){
    this.courseService.getCourse().subscribe(x => {this.courses = x;
      if (this.isDtInitialized) {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next();
        });
      } else {
        this.isDtInitialized = true
        this.dtTrigger.next();
      }
      this.changeDetectorRef.markForCheck();
    });
  }
  }
