import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnDestroy,
  OnInit,
  TemplateRef,
  ViewChild,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subject } from "rxjs";
import { IdentityControllersClaim } from "src/app/infrastructure/datum/claim/user-management";
import { DDLModel } from "src/app/infrastructure/model/UserManagement/ddl-model";
import { UserKycModel } from "src/app/infrastructure/model/UserManagement/user-kyc-model";
import { UserModel } from "src/app/infrastructure/model/UserManagement/user-model";
import { UserService } from "./service/user.service";
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: "app-user",
  templateUrl: "./user.component.html",
  styleUrls: ["./user.component.css"],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class UserComponent implements OnInit, OnDestroy {

  @ViewChild('kycModal') kycModalView : TemplateRef<any>; // Note: TemplateRef
  
  dtOptions: DataTables.Settings = {};
  persons: UserModel[] = [];
  dtTrigger: Subject<any> = new Subject<any>();
  kycDetail: UserKycModel;
  userCreateClaim = [IdentityControllersClaim.User.WriteUser];
  genderDDL: DDLModel[] =   [];
  maritalStatusDDL: DDLModel[] =   [];
  bloodGroupDDL: DDLModel[] =   [];

  closeResult: string;
  modalOptions:NgbModalOptions;
  
  constructor(
    private userService: UserService,
    private router: Router,
    private changeDetectorRef: ChangeDetectorRef,
    private route: ActivatedRoute,
    private modalService: NgbModal
  ) {
    this.modalOptions = {
      size: 'lg', backdrop: 'static'
   }
  }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: "full_numbers",
      pageLength: 5,
      lengthMenu: [
        [5, 10, 25, 50, 100, -1],
        [5, 10, 25, 50, 100, "All"],
      ],
      responsive: true,
    };
    this.userService.getUser().subscribe((x) => {
      this.persons = x;
      this.changeDetectorRef.markForCheck();
      this.dtTrigger.next();
    });

    var kycDDLData = this.route.snapshot.data.kycDDLData;
    this.genderDDL = kycDDLData.genderDDL;
    this.maritalStatusDDL = kycDDLData.maritalStatusDDL;
    this.bloodGroupDDL = kycDDLData.bloodGroupDDL;
  }
  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }
  hasEnable(id: string, val: any) {
    this.userService.enableDisableLogin(id, val.checked).subscribe(
      () => {
        this.changeDetectorRef.markForCheck();
      },
      () => {
        val.checked = !val.checked;
      }
    );
  }
  getEdit(id: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([`/dashboard/user/edit/${id}`])
    );
    window.open(url, "_self");
  }

  getKYC(id: string) {
    this.userService.getKYCDetail(id).subscribe((x) => {
      this.kycDetail = x;
      console.log(this.kycDetail);
      this.modalService.open(this.kycModalView, this.modalOptions);
    });
  }
}
