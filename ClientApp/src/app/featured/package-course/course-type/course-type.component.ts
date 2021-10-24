import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { CourseControllersClaim } from 'src/app/infrastructure/datum/claim/course-management';
import { CourseTypeModel } from 'src/app/infrastructure/model/UserManagement/resource/course/coursetypemodel';
import Swal from 'sweetalert2';
import { CourseTypeService } from '../service/course-type.service';

@Component({
  selector: 'app-course-type',
  templateUrl: './course-type.component.html',
  styleUrls: ['./course-type.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CourseTypeComponent implements OnInit,OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized:boolean = false;
  courseTypeCreateClaim = [CourseControllersClaim.CourseType.Write];
  courseTypes: CourseTypeModel[] = [];
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;

  constructor(private courseTypeService: CourseTypeService,
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
  
  getInitData(){
    this.courseTypeService.getCourseType().subscribe(x=>
      {
        this.courseTypes = x;
        if(this.isDtInitialized){
          this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
            dtInstance.destroy();
            this.dtTrigger.next();
          });
        } else{this.isDtInitialized = true
        this.dtTrigger.next();
      }
      this.changeDetectorRef.markForCheck();
    });
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  updateCourseTypeDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/config/coursetype/edit/${id}`])
    );
    window.open(url, "_self");
    //window.open(url, "_blank");
  }

  deleteCourseTypeDetail(id: string) {
    Swal.fire({
      title: "Delete Course type?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.courseTypeService.deleteCourseTypeById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Course type deleted successfully.",
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
}
