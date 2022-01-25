import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ShiftManagementClaim } from 'src/app/infrastructure/datum/claim/shift-management';
import { ShiftModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-model';
import Swal from 'sweetalert2';
import { ShiftService } from '../service/shift.service';

@Component({
  selector: 'app-shift',
  templateUrl: './shift.component.html',
  styleUrls: ['./shift.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ShiftComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized: boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  hasDataLoaded = false;
  shiftViewClaim = [ShiftManagementClaim.Shift.View];
  shiftWriteClaim = [ShiftManagementClaim.Shift.Write];

  shifts: ShiftModel[] = [];

  constructor(
    private router: Router,
    private shiftService: ShiftService,
    private changeDetectorRef: ChangeDetectorRef
  ) { }


  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      lengthMenu: [
        [5, 10, 25, 50, 100, -1],
        [5, 10, 25, 50, 100, "All"],
      ]
    };
    this.getInitData();
  }

  getInitData() {
    this.shiftService.getAllShift().subscribe(x => {
      this.shifts = x;
      if (this.isDtInitialized) {
        this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
          dtInstance.destroy();
          this.dtTrigger.next();
        });
      } else {
        this.isDtInitialized = true
        this.dtTrigger.next();
        this.hasDataLoaded = true;
      }
      this.changeDetectorRef.markForCheck();
    });
  }

  updateshift(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/shift-config/shift/edit/${id}`])
    );
    window.open(url, "_self");
  }

  deleteshift(id: string) {
    Swal.fire({
      title: "Delete shift?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.shiftService.deleteShiftById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Shift deleted successfully.",
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


  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
