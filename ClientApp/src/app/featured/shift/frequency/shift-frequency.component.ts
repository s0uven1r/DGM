import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ShiftService } from '../service/shift.service';
import { ShiftManagementClaim } from 'src/app/infrastructure/datum/claim/shift-management';
import { ShiftFrequencyModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-frequency-model';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-shift-frequency',
  templateUrl: './shift-frequency.component.html',
  styleUrls: ['./shift-frequency.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class ShiftFrequencyComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized:boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  hasDataLoaded = false;

  shiftViewClaim = [ShiftManagementClaim.ShiftFrequency.View];
  shiftWriteClaim = [ShiftManagementClaim.ShiftFrequency.Write];
  frequencies: ShiftFrequencyModel[] = [];
  
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
  
  getInitData(){
    this.shiftService.getAllShiftFrequency().subscribe(x=>
      {
        this.frequencies = x;
        if(this.isDtInitialized){
          this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
            dtInstance.destroy();
            this.dtTrigger.next();
          });
        } else{this.isDtInitialized = true
        this.dtTrigger.next();
        this.hasDataLoaded = true;
      }
      this.changeDetectorRef.markForCheck();
    });
  }

  updateshiftFrequency(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/shift-config/frequency/edit/${id}`])
    );
    window.open(url, "_self");
  }

  deleteshiftFrequency(id: string) {
    Swal.fire({
      title: "Delete shift frequency?",
      text: "",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.shiftService.deleteShiftFrequencyById(id).subscribe(
          () => {
            Swal.fire(
              "Deleted!",
              "Shift frequency deleted successfully.",
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
