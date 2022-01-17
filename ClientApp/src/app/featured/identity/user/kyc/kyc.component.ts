import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { DDLModel } from "src/app/infrastructure/model/UserManagement/ddl-model";
import Swal from "sweetalert2";
import { UserService } from "../service/user.service";

@Component({
  selector: "app-kyc",
  templateUrl: "./kyc.component.html",
  styleUrls: ["./kyc.component.css"],
})
export class KycComponent implements OnInit {
  InternalUserForm: FormGroup;
  genderDDL: DDLModel[] = [];
  maritalStatusDDL: DDLModel[] = [];
  bloodGroupDDL: DDLModel[] = [];
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private form: FormBuilder
  ) {
    this.InternalUserFormDesign();
  }

  InternalUserFormDesign() {
    return (this.InternalUserForm = this.form.group({
      fullname: [null, Validators.required],
      permanentAddress: [null, Validators.required],
      temporaryAddress: [null, Validators.required],
      contactNumber: [null, Validators.required],
      alternativeContactNumber: [null],
      citizenshipNumber: [null, Validators.required],
      panNumber: [null],
      gender: [null, Validators.required],
      maritalStatus: [null, Validators.required],
      fathersName: [null, Validators.required],
      mothersName: [null, Validators.required],
      spouseName: [null],
      child1Name: [null],
      child2Name: [null],
      bloodGroup: [null, Validators.required],
      anyMedication: [false],
      anyMedicalCondition: [false],
    }));
  }

  ngOnInit(): void {
    var kycDDLData = this.route.snapshot.data.kycDDLData;
    this.genderDDL = kycDDLData.genderDDL;
    this.maritalStatusDDL = kycDDLData.maritalStatusDDL;
    this.bloodGroupDDL = kycDDLData.bloodGroupDDL;
  }

  updateKyc() {
    Swal.fire({
      title: "Update KYC",
      text: "User Action",
      icon: "warning",
      showCancelButton: true,
      confirmButtonText: "Ok",
      cancelButtonText: "No",
    }).then((result) => {
      if (result.value) {
        this.userService
          .updateKyc(this.InternalUserForm.value)
          .pipe(
            catchError((err) => {
              return throwError(err);
            })
          )
          .subscribe(
            () => {
              Swal.fire("Updated", "User Action", "success");
            },
            () => console.log("HTTP request completed.")
          );
      }
    });
  }
}
