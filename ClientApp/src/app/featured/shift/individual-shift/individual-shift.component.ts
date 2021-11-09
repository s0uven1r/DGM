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
import { Observable, of, Subject } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs/operators';
import { ShiftManagementClaim } from '../../../infrastructure/datum/claim/shift-management';
import { IndividualShiftModel } from '../../../infrastructure/model/UserManagement/resource/shift/individual-shift-model';
import { UserService } from '../../identity/user/service/user.service';
import { ShiftService } from '../service/shift.service';

@Component({
  selector: 'app-individual-shift',
  templateUrl: './individual-shift.component.html',
  styleUrls: ['./individual-shift.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class IndividualShiftComponent implements OnInit, OnDestroy {
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized:boolean = false;
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;
  hasDataLoaded = false;
  individualShiftWriteClaim = [ShiftManagementClaim.IndividualShift.Write];
  shifts: IndividualShiftModel[] = [];
  result = [];
  resultPair = [];
  constructor(
    private router: Router,
    private userService: UserService,
    private shiftService: ShiftService,
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
  }
  
  getInitData(accountNo: string){
    this.shiftService.getAllByAccountNumber(accountNo).subscribe(x=>
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
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  searchItem(index?: number) {
    return (text$: Observable<string>) => {
      return text$.pipe(
        debounceTime(300),
        distinctUntilChanged(),
        switchMap(term => this.searchGetCall(term, index).pipe(
          catchError(() => {
            return of([]);
          })))
      )
    }
}
search = (text$: Observable<string>) => {
  return text$.pipe(
    debounceTime(300),
    distinctUntilChanged(),
    switchMap(term => this.searchGetCall(term).pipe(
      catchError(() => {
        return of([]);
      })))
  )
}

searchGetCall(term: string, _index?: number) {
  if (term === '') {
    return of([]);
  }
    return this.userService.getAllAccountDetails(term).pipe(map(x => {
      this.result = [];
       x.forEach((element: { key: any; }) => {
         this.result.push(element.key);
       });
       this.resultPair.push(x);
       this.changeDetectorRef.markForCheck();
       return this.result;
    }));
  }

  formatter = (result: string) => result.toUpperCase();
  selectedItem(item: { item: string; }){
      var accNo = item.item.split('-')[1].trim();
      this.getInitData(accNo);
    }
    updateShiftDetail(id: string){
        const url = this.router.serializeUrl(
          this.router.createUrlTree([`/dashboard/shift-config/individual-shift/edit/${id}`])
        );
        window.open(url, "_self");
    }
}
