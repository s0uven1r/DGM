import { formatDate } from '@angular/common';
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { Observable, of, Subject, throwError } from 'rxjs';
import { AccountControllersClaim } from 'src/app/infrastructure/datum/claim/account-management';
import { TransactionEntryModel } from 'src/app/infrastructure/model/UserManagement/resource/account/transaction-entry-model';
import { AccountService } from '../service/account.service';

@Component({
  selector: 'app-transaction-entry',
  templateUrl: './transaction-entry.component.html',
  styleUrls: ['./transaction-entry.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class TransactionEntryComponent implements OnInit, OnDestroy {
  accountEntryViewClaim = [AccountControllersClaim.AccountEntry.View];
  accountEntryCreateClaim = [AccountControllersClaim.AccountEntry.Write];
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject<any>();
  isDtInitialized: boolean = false;
  journalEntries: TransactionEntryModel[] = [];
  @ViewChild(DataTableDirective)
  dtElement: DataTableDirective;

  constructor(private accountService: AccountService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef
  ) { }

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

  getInitData() {
    this.accountService.getAllTransactionEntries().subscribe(x => {
      this.journalEntries = x;
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

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  updateTransactionEntryDetail(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/account/transactionentry/edit/${id}`])
    );
    window.open(url, "_self");
  }
}