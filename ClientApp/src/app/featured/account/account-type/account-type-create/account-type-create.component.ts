import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AccountTypeDDL } from "src/app/infrastructure/model/UserManagement/resource/account/account-type-model";
import Swal from "sweetalert2";
import { AccountService } from "../../service/account.service";

@Component({
  selector: "app-account-type-create",
  templateUrl: "./account-type-create.component.html",
  styleUrls: ["./account-type-create.component.css"],
})
export class AccountTypeCreateComponent implements OnInit {
  createForm: FormGroup;
  accountTypeDDL: AccountTypeDDL[] = [];
  constructor(
    private accountService: AccountService,
    private form: FormBuilder, 
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.accountTypeDDL = this.route.snapshot.data.accountTypeDDL;
  }

  FormDesign() {
    return (this.createForm = this.form.group({
      title: [null, Validators.required],
      type: ['', Validators.required],
    }));
  }

  createAccountType() {
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
          .createAccountType(this.createForm.value)
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
              const url = this.router.serializeUrl(
                this.router.createUrlTree([`/dashboard/account/accounttype`])
              );
              window.open(url, "_self");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }
}
