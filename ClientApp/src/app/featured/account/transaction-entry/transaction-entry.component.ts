import { formatDate } from '@angular/common';
import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import NepaliDate from 'nepali-date-converter';
import { Observable, of, Subject, throwError } from 'rxjs';
import { catchError, debounceTime, distinctUntilChanged, map, switchMap, filter } from 'rxjs/operators';
import Swal from 'sweetalert2';
import { UserService } from '../../identity/user/service/user.service';
import { VehicleService } from '../../vehicle/service/vehicle.service';
import { AccountService } from '../service/account.service';

@Component({
  selector: 'app-transaction-entry',
  templateUrl: './transaction-entry.component.html',
  styleUrls: ['./transaction-entry.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})

export class TransactionEntryComponent implements OnInit, AfterViewInit {
  @ViewChild('dateEntryVal', { static: true }) dateVal: ElementRef;
  entryForm: FormGroup;
  errorMsg = '';
  balanceErrorMsg = '';
  debitPaidAmount = 0;
  creditPaidAmount = 0;
  result = [];
  resultPair = [];
  focus$ = [0, 1].map(_ => new Subject<string>());

  constructor(private form: FormBuilder,
    private changeDetectorRef: ChangeDetectorRef,
    private accountService: AccountService,
    private vehicleService: VehicleService,
    private userService: UserService) {
    this.FormDesign();
  }

  ngOnInit(): void {
  }
  ngAfterViewInit() {
    this.designDatePicker();
  }

  designDatePicker() {
    var div = (document.getElementsByClassName('datePickerDiv')) as HTMLCollection;
    var i = 0;
    for (i = 0; i < div.length; i++) {
      div[i].children[0].children[0].className = "";
      div[i].children[0].children[0].className = "form-control";
    }
  }
  FormDesign() {
    return (this.entryForm = this.form.group({
      title: [null, Validators.required],
      accountNumber: [null, Validators.required],
      type: [null, Validators.required],
      entryDateNP: [null, Validators.required],
      entryDateEN: [null, Validators.required],
      marketPrice: [null, Validators.required],
      discountAmount: [null],
      netAmount: [null, Validators.required],
      netAmountLabel: [null, Validators.required],
      dueAmount: [null],
      dueAmountLabel: [null],
      remarks: [null],
      journalEntry: this.form.array([], null)
    }));
  }

  journalEntries(): FormArray {
    let journal = this.entryForm.get('journalEntry') as FormArray;
    setInterval(() => {
      this.designDatePicker();
    }, 1000);

    this.changeDetectorRef.markForCheck();
    return journal;
  }
  newJournalEntry(): FormGroup {
    return this.form.group({
      title: [null, Validators.required],
      accountNumber: [null, Validators.required],
      type: [null, Validators.required],
      entryDateNP: [null, Validators.required],
      entryDateEN: [null, Validators.required],
      debitAmount: [null, Validators.required],
      creditAmount: [null, Validators.required],
      remarks: [null]
    });
  }
  addJournalEntry() {
    this.journalEntries().push(this.newJournalEntry());
  }
  removeJournalEntry(i: number) {
    this.journalEntries().removeAt(i);
    this.calculateTotal();
  }
  calculateTotal() {
    this.errorMsg = '';
    this.balanceErrorMsg = '';
    var mrp = this.entryForm.get('marketPrice').value;
    var discount = this.entryForm.get('discountAmount').value;
    var netAmount = mrp - discount;
    this.debitPaidAmount = this.entryForm.get('journalEntry').value.reduce((acc: any, cur: { debitAmount: any; }) => acc + cur.debitAmount, 0);
    this.creditPaidAmount = this.entryForm.get('journalEntry').value.reduce((acc: any, cur: { creditAmount: any; }) => acc + cur.creditAmount, 0);
    this.entryForm.patchValue({
      netAmount: netAmount,
      dueAmount: (netAmount - this.debitPaidAmount),
      netAmountLabel: "Net Amount Rs." + netAmount,
      dueAmountLabel: "Due Amount Rs." + (netAmount - this.debitPaidAmount)
    });
    if (this.debitPaidAmount !== this.creditPaidAmount) {
      this.balanceErrorMsg = `Sum of debit and credit amount should equal to each others Debit Amount Rs.${this.debitPaidAmount}, Credit Amount Rs.${this.creditPaidAmount}`;
    }
    else if (netAmount < this.debitPaidAmount) {
      this.errorMsg = `No due amount for this transaction. Due Amount Rs. ${netAmount - this.debitPaidAmount}`;
    }
    this.changeDetectorRef.markForCheck();
  }
  getAccountNumber() {
    this.entryForm.reset();
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
    var type = '';
    if (_index !== null && _index !== undefined) {
      type = this.journalEntries().controls[_index]['controls']['type'].value
    } else {
      type = this.entryForm.controls['type'].value;
    }
    if (type === '3') {
      return this.accountService.getAllAccountHeadAccountDetails(term).pipe(map(x => {
        this.result = [];
        x.forEach((element: { key: any; }) => {
          this.result.push(element.key);
        });
        this.resultPair.push(x);
        this.changeDetectorRef.markForCheck();
        return this.result;
      }));
    } else if (type === '2') {
      return this.vehicleService.getAllAccountDetails(term).pipe(map(x => {
        this.result = [];
        x.forEach((element: { key: any; }) => {
          this.result.push(element.key);
        });
        this.resultPair.push(x);
        this.changeDetectorRef.markForCheck();
        return this.result;
      }));
    } else if (type === '1') {
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
    else {
      return of([]);
    }
  }
  selectedItem(item: { item: string; }) {
    var accNo = item.item.split('-')[1].trim();
    this.entryForm.controls['accountNumber'].setValue(accNo);
  }
  selectedJournalItem(item: { item: string; }, index: number) {
    var accNo = item.item.split('-')[1].trim();
    this.journalEntries().controls[index].patchValue({
      accountNumber: accNo
    });
  }
  formatter = (result: string) => result.toUpperCase();
  changeNepaliToEnglish(val: { formattedDate: string; }) {
    var dateValue = val.formattedDate;
    if (dateValue) {
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1];
      var d: number = +date[0];
      var actualDate = new NepaliDate(y, m, d);
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      val.formattedDate = npDate;

      require('nepali-date-converter');
      var dateAd = formatDate(actualDate.toJsDate(), "dd/MM/yyyy", "en-US");
      this.entryForm.patchValue({
        entryDateEN: dateAd
      });

      this.getTransactionEntry();
    }
  }
  changeEntryNepaliToEnglish(val: { formattedDate: string; }, index: number) {
    var dateValue = val.formattedDate;
    if (dateValue) {
      var date = dateValue.split("/", 3);
      var y: number = +date[2];
      var m: number = +date[1];
      var d: number = +date[0];
      var actualDate = new NepaliDate(y, m, d);
      var npDate = actualDate.format('DD/MM/YYYY', 'np').toString();
      val.formattedDate = npDate;

      require('nepali-date-converter');
      var dateAd = formatDate(actualDate.toJsDate(), "dd/MM/yyyy", "en-US");
      this.journalEntries().controls[index].patchValue({
        entryDateEN: dateAd
      });
    }
  }
  createTransactionEntry() {
    Swal.fire({
      title: "Add account entry",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.accountService
          .createTransactionEntry(this.entryForm.value)
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
  getTransactionEntry() {
    let typeVal = this.entryForm.get('type').value;
    let accountNumberVal = this.entryForm.get('accountNumber').value;
    let transactionDateENVal = this.entryForm.get('entryDateEN').value;

    if (typeVal  && accountNumberVal  && transactionDateENVal) {
      this.accountService.getTransactionEntry(typeVal, accountNumberVal, transactionDateENVal)
      .subscribe((x) => {
        if(!x)
        return;
        
        console.log(x);
        this.entryForm.patchValue({
          id: x.id,
          accountNumber: x.accountNumber,
          discountAmount: x.discountAmount,
          dueAmount: x.dueAmount,
          marketPrice: x.marketPrice,
          netAmount: x.netAmount,
          remarks: x.remarks,
          type: x.type
        });

        x.journalEntries.forEach(element => {
          debugger;
          var date = element.entryDateNP.split("/", 3);
          var y: number = +date[2];
          var m: number = +date[1];
          var d: number = +date[0];
          var actualDate = new NepaliDate(y, m, d);

          this.journalEntries().push(
            this.form.group({
              title: null,
              accountNumber: element.accountNumber,
              type: element.type,
              entryDateNP: actualDate,
              entryDateEN: element.entryDateEN,
              debitAmount: element.debitAmount,
              creditAmount: element.creditAmount,
              remarks: element.remarks
            })
          );
          this.changeDetectorRef.markForCheck();
          debugger;
          this.dateVal['formattedDate'] = actualDate.format('DD/MM/YYYY', 'np').toString();
        });
      });
    }
  }
}
