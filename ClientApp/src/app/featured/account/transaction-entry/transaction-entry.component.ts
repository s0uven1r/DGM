import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-transaction-entry',
  templateUrl: './transaction-entry.component.html',
  styleUrls: ['./transaction-entry.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TransactionEntryComponent implements OnInit {
  entryForm: FormGroup;
  errorMsg = '';
  balanceErrorMsg = '';
  debitPaidAmount = 0;
  creditPaidAmount = 0;
  constructor(private form: FormBuilder, private changeDetectorRef: ChangeDetectorRef) {
      this.FormDesign();
    }

  ngOnInit(): void {
  }
  FormDesign() {
    return (this.entryForm = this.form.group({
      title: [null, Validators.required],
      type: [null, Validators.required],
      entryDate: [null, Validators.required],
      marketPrice: [null, Validators.required],
      discountAmount: [null],
      netAmount: [null, Validators.required],
      dueAmount: [null],
      remarks: [null],
      journalEntry: this.form.array([this.newJournalEntry()])
    }));
  }
  journalEntries(): FormArray {
    return this.entryForm.get('journalEntry') as FormArray;
  }

  newJournalEntry(): FormGroup {
    return this.form.group({
      title: [null, Validators.required],
      entryDate:[null, Validators.required],
      debitAmount:[null, Validators.required],
      creditAmount:[null, Validators.required],
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

  calculateTotal(){
    this.errorMsg = '';
    this.balanceErrorMsg ='';
    var mrp = this.entryForm.get('marketPrice').value;
    var discount = this.entryForm.get('discountAmount').value;
    var netAmount = mrp - discount;
    this.debitPaidAmount = this.entryForm.get('journalEntry').value.reduce((acc: any, cur: { debitAmount: any; }) => acc + cur.debitAmount, 0);
    this.creditPaidAmount = this.entryForm.get('journalEntry').value.reduce((acc: any, cur: { creditAmount: any; }) => acc + cur.creditAmount, 0);
    this.entryForm.patchValue({
      netAmount: "Net Amount Rs."+ netAmount,
      dueAmount: "Due Amount Rs."+ (netAmount - this.debitPaidAmount)
    });
    if(this.debitPaidAmount !== this.creditPaidAmount){
      this.balanceErrorMsg= `Sum of debit and credit amount is not equal to each others Debit Amount Rs.${this.debitPaidAmount}, Credit Amount Rs.${this.creditPaidAmount}`;
    }
    else if(netAmount < this.debitPaidAmount){
     this.errorMsg = `No due amount for this transaction. Due Amount Rs. ${netAmount-this.debitPaidAmount}`;
    }
    this.changeDetectorRef.markForCheck();
  }
}
