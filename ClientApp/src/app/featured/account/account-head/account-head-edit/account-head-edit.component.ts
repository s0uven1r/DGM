import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { throwError } from "rxjs/internal/observable/throwError";
import { catchError } from "rxjs/operators";
import { AccountTypeModel } from "src/app/infrastructure/model/UserManagement/resource/account/account-type-model";
import Swal from "sweetalert2";
import { AccountService } from "../../service/account.service";

@Component({
  selector: "app-account-head-edit",
  templateUrl: "./account-head-edit.component.html",
  styleUrls: ["./account-head-edit.component.css"],
})
export class AccountHeadEditComponent implements OnInit {
  editForm: FormGroup;
  accountTypeDDL: AccountTypeModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private form: FormBuilder
  ) {
    this.FormDesign();
  }

  ngOnInit(): void {
    this.accountTypeDDL = this.route.snapshot.data.accountTypeDDL;
    this.getInitData();
  }

  FormDesign() {
    return (this.editForm = this.form.group({
      id: [null, Validators.required],
      title: [null, Validators.required],
      accountTypeId: [null, Validators.required],
    }));
  }

  editAccountHead() {
    Swal.fire({
      title: "Update Account Head Detail",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.accountService
          .updateAccountHead(this.editForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              this.editForm.reset();
              this.editForm.clearValidators();
              Swal.fire("Updated!", "User Action", "success");
              const url = this.router.serializeUrl(
                this.router.createUrlTree([`/dashboard/account/accounthead`])
              );
              window.open(url, "_self");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }

  getInitData() {
    this.route.params.subscribe((params) => {
      if (params["id"]) {
        this.accountService.getByIdAccountHead(params["id"]).subscribe((x) => {
          this.editForm.patchValue({
            id: x.id,
            title: x.title,
            accountTypeId: x.accountTypeId,
          });
        });
      }
    });
  }
}
