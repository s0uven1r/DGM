import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AccountTypeModel } from 'src/app/infrastructure/model/UserManagement/resource/account/account-type-model';
import Swal from 'sweetalert2';
import { AccountService } from '../../service/account.service';

@Component({
  selector: 'app-account-head-create',
  templateUrl: './account-head-create.component.html',
  styleUrls: ['./account-head-create.component.css']
})
export class AccountHeadCreateComponent implements OnInit {
  createForm: FormGroup;
  accountTypeDDL: AccountTypeModel[];
  constructor(
    private accountService: AccountService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.getInitData();
  }

  FormDesign() {
    return (this.createForm = this.form.group({
      title: [null, Validators.required],
      accountTypeId: [null, Validators.required],
    }));
  }

  createAccountHead() {
    Swal.fire({
      title: "Add Account Type Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.accountService
          .createAccountHead(this.createForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              this.createForm.reset();
              this.createForm.clearValidators();
              Swal.fire("Added!", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }

  getInitData() {
    this.accountService.getAllAccountType().subscribe((x) => {
      this.accountTypeDDL = x;
    });
  }
}
