import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Subject } from 'rxjs';
import { ShiftService } from '../service/shift.service';
import { ShiftManagementClaim } from 'src/app/infrastructure/datum/claim/shift-management';
import { ShiftModel } from 'src/app/infrastructure/model/UserManagement/resource/shift/shift-model';

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
  shiftViewClaim = [ShiftManagementClaim.Shift.View];
  shiftWriteClaim = [ShiftManagementClaim.Shift.Write];
  shifts: ShiftModel[] = [];
  
  constructor(
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
    this.shiftService.getAllShift().subscribe(x=>
      {
        this.shifts = x;
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

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }

}
